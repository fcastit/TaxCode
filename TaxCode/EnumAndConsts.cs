using System.Text.RegularExpressions;

namespace TaxCode
{
    public static class EnumAndConsts
    {
        #region Constants

        public const string X_CHAR = "X";

        public static readonly char[] CHECK_CODE = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static readonly int[,] EVEN_ODD_CHAR_CODES = new int[2, 36]{
            {0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25},
            {1,0,5,7,9,13,15,17,19,21,1,0,5,7,9,13,15,17,19,21,2,4,18,20,11,3,6,8,12,14,16,10,22,25,24,23}
        };

        public static readonly char[] MONTHS_CODES = {
            'A', 'B', 'C', 'D', 'E', 'H',
            'L', 'M', 'P', 'R', 'S', 'T'
        };

        public static readonly Regex PATTERN_VOWELS = new Regex("[AEIOU]", RegexOptions.IgnoreCase);

        #endregion
    }
}
