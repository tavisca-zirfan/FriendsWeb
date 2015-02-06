using System;
using System.Threading;

namespace Infrastructure.Common
{
    public class RainDrop
    {
        private static long _lastMilliseconds = long.MaxValue;
        private static long _sequence;

        //Time: 1st August 2012
        private const long EpochTime = 634793760000000000;
        private const int ServerIdBits = 9;
        private const int DataCenterIdBits = 3;
        private const int SequenceBits = 8;

        private const int ServerIdShift = SequenceBits;
        private const int DataCenterIdShift = SequenceBits + ServerIdBits;
        private const int TimeShift = SequenceBits + ServerIdBits + DataCenterIdBits;

        public string GetNextId()
        {
            return GetNextId(1, 1);
        }

        public string GetNextId(long serverId, long dataCenterId)
        {
            var currentSequence = NextSeq();
            var lastMilliSeconds = Interlocked.Read(ref _lastMilliseconds);
            var currentMilliSeconds = GetSystemMilliSeconds();

            Interlocked.CompareExchange(ref _lastMilliseconds, currentMilliSeconds, lastMilliSeconds);

            if (lastMilliSeconds > currentMilliSeconds)
            {
                GetNextId(serverId, dataCenterId);
            }
            return IdEncoder.Minify(GenerateId(currentMilliSeconds, currentSequence, serverId, dataCenterId));
        }

        private static long NextSeq()
        {
            Interlocked.CompareExchange(ref _sequence, -1, 255);

            return Interlocked.Increment(ref _sequence);
        }

        private static long GenerateId(long milliSeconds, long sequence, long serverId, long dataCenterId)
        {
            var dataCenter = dataCenterId << DataCenterIdShift;

            var server = serverId << ServerIdShift;

            long id = milliSeconds | dataCenter | server | sequence;
            return id;
        }

        private static long GetSystemMilliSeconds()
        {
            var epochTime = new DateTime(2012, 8, 1).Ticks;
            var now = DateTime.UtcNow.Ticks;
            var time = (now - epochTime) / TimeSpan.TicksPerMillisecond;
            return time << TimeShift;
        }

        private static void EpochTimeGet()
        {

            var epochTime = DateTime.Parse("1 Aug 2012");
            Console.WriteLine(epochTime.Ticks);
        }
    }


    public static class IdEncoder
    {

        private static readonly char[] Base36 = new[] { '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
        public static string Minify(long value)
        {
            return IntToStringFast(value, Base36);
        }

        /// <summary>
        /// An optimized method using an array as buffer instead of 
        /// string concatenation. This is faster for return values having 
        /// a length > 1.
        /// </summary>
        public static string IntToStringFast(long value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int bufferSize, i;
            bufferSize = i = 128;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[bufferSize - i];
            Array.Copy(buffer, i, result, 0, bufferSize - i);

            return new string(result);
        }
    }
}
