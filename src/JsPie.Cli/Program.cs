using JsPie.Plugins.Keyboard;
using System.Windows.Forms;
using JsPie.Core;
using JsPie.Scripting;
using JsPie.Runtime;
using System.Collections.Generic;
using JsPie.Plugins.Ps3;
using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using JsPie.Plugins.VJoy;

namespace JsPie.Cli
{
    class Program
    {
        static KeyboardPlugin _keyboardPlugin;
        static Ps3Plugin _ps3Plugin;
        static VJoyPlugin _vJoyPlugin;
        static ConcurrentQueue<IScriptInput> _queue;
        static AutoResetEvent _event;
        static IJsPieServiceProvider _serviceProvider;
        static Task _scriptTask;

        static object _lockObject;

        static void Main(string[] args)
        {
            _lockObject = new object();
            _queue = new ConcurrentQueue<IScriptInput>();
            _event = new AutoResetEvent(false);

            _queue.Enqueue(new ScriptInput(new ControlEvent[0]));

            using (_keyboardPlugin = new KeyboardPlugin())
            using (_ps3Plugin = new Ps3Plugin())
            using (_vJoyPlugin = new VJoyPlugin())
            {
                _keyboardPlugin.ControlEvent += OnControlEvent;
                _keyboardPlugin.ControlEvents += OnControlEvents;

                _ps3Plugin.ControlEvent += OnControlEvent;
                _ps3Plugin.ControlEvents += OnControlEvents;

                var serviceProvider = new DefaultJsPieServiceProvider();
                serviceProvider.Register<IScriptConsole>(() => new ScriptConsole(ScriptSeverity.Info));
                var settings = new ScriptEngineSettings(args.Length > 0 ? args[0] : "D:\\Development\\JsPie\\test.js");
                serviceProvider.Register(() => settings);
                var directory = new ControllerDirectory(
                    new IInputPlugin[] { _keyboardPlugin, _ps3Plugin }.SelectMany(p => p.GetControllers()), 
                    new IOutputPlugin[] { _keyboardPlugin, _vJoyPlugin }.SelectMany(p => p.GetControllers()));
                serviceProvider.Register(() => directory);
                _serviceProvider = serviceProvider;
                _scriptTask = Task.Factory.StartNew(Run);

                Application.Run();
            }
        }

        private static void OnControlEvent(object sender, ControlEvent e)
        {
            _queue.Enqueue(new ScriptInput(e));
            _event.Set();
        }

        private static void OnControlEvents(object sender, IReadOnlyList<ControlEvent> e)
        {
            _queue.Enqueue(new ScriptInput(e));
            _event.Set();
        }

        private static void Run()
        {
            using (var engine = _serviceProvider.GetRequiredService<IScriptEngine>())
            {
                engine.Initialize();

                while (true)
                {
                    IScriptInput input;
                    if (_queue.TryDequeue(out input))
                    {
                        var output = engine.Run(input);
                        if (output.WasSuccessful && output.HasValue)
                        {
                            _keyboardPlugin.ProcessEvents(output.Value.ControlEvents);
                            _vJoyPlugin.ProcessEvents(output.Value.ControlEvents);
                        }
                        continue;
                    }

                    _event.WaitOne();
                }
            }
        }
    }
}
