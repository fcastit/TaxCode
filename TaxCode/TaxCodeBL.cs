using System.Reflection;
using System.Text;

namespace TaxCode
{
    public static class TaxCodeBL
    {
        public static string GetTaxCode(string lastName, string firstName, string dateOfBirth, string placeOfBirth, string gender)
        {
            var taxCode = new StringBuilder();

            taxCode.Append(GetLastNamePart(lastName));
            taxCode.Append(GetFirstNamePart(firstName));
            taxCode.Append(GetDateOfBirthPart(dateOfBirth, gender));
            taxCode.Append(GetPlaceOfBirthPart(placeOfBirth));
            taxCode.Append(GetLastChar(taxCode.ToString()));

            return taxCode.ToString();
        }

        private static string GetLastNamePart(string lastName)
        {
            var lastNamePart = new StringBuilder();

            lastName = lastName.Trim();

            lastNamePart.Append(GetConsVowels(lastName, true));
            lastNamePart.Append(GetConsVowels(lastName, false));

            if (lastNamePart.Length > 3)
            {
                lastNamePart.Length = 3;
            }

            while (lastNamePart.Length < 3)
            {
                lastNamePart.Append(EnumAndConsts.X_CHAR);
            }

            return lastNamePart.ToString().ToUpper();
        }

        private static string GetFirstNamePart(string firstName)
        {
            var firstNamePart = new StringBuilder();

            firstName = firstName.Trim();

            var consonants = GetConsVowels(firstName, true);

            if (consonants.Length >= 4)
            {
                firstNamePart.Append(consonants[0]);
                firstNamePart.Append(consonants.Substring(2, 2));
            }
            else
            {
                firstNamePart.Append(consonants);
            }

            firstNamePart.Append(GetConsVowels(firstName, false));

            if (firstNamePart.Length > 3)
            {
                firstNamePart.Length = 3;
            }

            while (firstNamePart.Length < 3)
            {
                firstNamePart.Append(EnumAndConsts.X_CHAR);
            }

            return firstNamePart.ToString().ToUpper();
        }

        private static string GetDateOfBirthPart(string dateOfBirth, string gender)
        {
            var dateOfBirthPart = new StringBuilder();

            try
            {
                var dateOfBirthParts = dateOfBirth.Split('-');

                dateOfBirthPart.Append(dateOfBirthParts[0].Substring(2));
                dateOfBirthPart.Append(EnumAndConsts.MONTHS_CODES[Convert.ToInt32(dateOfBirthParts[1]) - 1]);
                if (gender.Equals("F", StringComparison.InvariantCultureIgnoreCase))
                {
                    dateOfBirthParts[2] = (Convert.ToInt32(dateOfBirthParts[2]) + 40).ToString();
                }
                dateOfBirthPart.Append(dateOfBirthParts[2]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error parsing date of birth", ex);
            }

            return dateOfBirthPart.ToString();
        }

        private static string GetPlaceOfBirthPart(string placeOfBirth)
        {
            string placePart = String.Empty;
            string taxCodesFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\TaxCodes.txt");

            using (StreamReader file = File.OpenText(taxCodesFilePath))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts[0].Equals(placeOfBirth, StringComparison.InvariantCultureIgnoreCase))
                    {
                        placePart = parts[1];
                        break;
                    }
                }
            }

            if (String.IsNullOrEmpty(placePart))
            {
                throw new Exception("Place not found");
            }

            return placePart;
        }

        private static string GetLastChar(string taxCode)
        {
            int sum = 0;

            for (int i = 0; i < taxCode.Length; i++)
            {
                var k = Convert.ToInt32(Char.GetNumericValue(taxCode[i]));

                if (k == -1)
                {
                    k = taxCode[i] - 'A' + 10;
                }

                if (i % 2 == 0)
                {
                    sum += EnumAndConsts.EVEN_ODD_CHAR_CODES[1, k];
                }
                else
                {
                    sum += EnumAndConsts.EVEN_ODD_CHAR_CODES[0, k];
                }
            }

            sum = sum % 26;

            return EnumAndConsts.CHECK_CODE[sum].ToString();
        }

        private static string GetConsVowels(string input, bool consonant)
        {
            var text = new StringBuilder();

            foreach (var character in input)
            {
                if (IsVowel(character) ^ consonant)
                {
                    text.Append(character);
                }
            }

            return text.ToString();
        }

        private static bool IsVowel(char character)
        {
            return EnumAndConsts.PATTERN_VOWELS.IsMatch(character.ToString());
        }
    }
}
