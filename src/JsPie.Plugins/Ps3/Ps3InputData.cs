using JsPie.Core.Util;

namespace JsPie.Plugins.Ps3
{
    public class Ps3InputData
    {
        private readonly byte[] _buffer;

        private int _dPadValue;

        public Ps3InputData(byte[] buffer)
        {
            _buffer = Guard.NotNull(buffer, nameof(buffer));

            Reset();
        }

        public byte GetByte(int index)
        {
            return _buffer[index];
        }

        public bool GetBit(int index, int bitMask)
        {
            return (_buffer[index] & bitMask) != 0;
        }

        public int GetDPad()
        {
            if (_dPadValue >= 0)
                return _dPadValue;

            return _dPadValue = GetByte(5) & 14;
        }

        internal void Reset()
        {
            _dPadValue = -1;
        }
    }
}
