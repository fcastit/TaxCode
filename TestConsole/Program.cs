namespace TaxCode
{
    public static void Main()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter date of birth (YYYY-MM-DD): ");
        string dateOfBirth = Console.ReadLine();

        Console.Write("Enter place of birth: ");
        string placeOfBirth = Console.ReadLine();

        Console.Write("Enter gender (M/F): ");
        string gender = Console.ReadLine();

        try
        {
            string taxCode = TaxCodeBL.GetCodiceFiscale(lastName, firstName, dateOfBirth, placeOfBirth, gender);
            Console.WriteLine($"The generated tax code is: {taxCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
