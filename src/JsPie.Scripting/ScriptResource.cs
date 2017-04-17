using JsPie.Core.Util;

namespace JsPie.Scripting
{
    public class ScriptResource
    {
        public string Path { get; }
        public string Name { get; }
        public byte[] Content { get; }

        public ScriptResource(string path, string name, byte[] content)
        {
            Path = Guard.NotNullOrEmpty(path, nameof(path));
            Name = Guard.NotNullOrEmpty(name, nameof(name));
            Content = Guard.NotNull(content, nameof(content));
        }
    }
}
