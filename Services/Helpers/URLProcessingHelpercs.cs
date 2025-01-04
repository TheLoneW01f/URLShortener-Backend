using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace URLShortener.Services.Helpers
{
    public class URLProcessingHelper
    {
        public static string Encode(Guid guid, string Characters, int shortCodeLength)
        {
            using var sha256 = SHA256.Create();
            var guidBytes = guid.ToByteArray();
            var hashBytes = sha256.ComputeHash(guidBytes);

            // Convert hash bytes to a numeric value
            var numericValue = new BigInteger(hashBytes, true, true); // Treat as unsigned, big-endian
            var base62 = new StringBuilder();

            // Convert numeric value to Base62
            while (numericValue > 0)
            {
                base62.Insert(0, Characters[(int)(numericValue % 62)]);
                numericValue /= 62;
            }

            #region BIG WARNING !!!!!
            /*
             * Truncating a GUID based hash to fixed length will lose uniqueness and thus can cause collisions. 
             * 6 Characters should give us 50Billion unique codes but may not guarantee uniqueness. Hence we need to check while inserting
             */

            // Ensure the output matches the desired length
            return base62.Length > shortCodeLength
                ? base62.ToString()[..shortCodeLength] // Truncate
                : base62.ToString().PadLeft(shortCodeLength, '0'); // Pad with zeros
            #endregion
        }
    }
}
