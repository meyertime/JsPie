namespace JsPie.Core.Util
{
    public static class HashingExtensions
    {
        public static int MixJenkins32(this int @this, int that)
        {
            int result = @this + that;

            result += (result << 12);
            result ^= (result >> 22);
            result += (result << 4);
            result ^= (result >> 9);
            result += (result << 10);
            result ^= (result >> 2);
            result += (result << 7);
            result ^= (result >> 12);

            return result;
        }

        public static int MixHashCode(this int @this, object that)
        {
            int hash;

            if (ReferenceEquals(that, null))
            {
                hash = 0;
            }
            else
            {
                hash = that.GetHashCode();
            }

            return @this.MixJenkins32(hash);
        }
    }
}
