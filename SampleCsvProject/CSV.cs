using System;
using System.Collections.Generic;
using System.IO;

public static class CSV
{

    //I thought of another way to connect to the API of names and from there and randomly 

    /*
    private const string MaleFirstNameUrl = "https://raw.githubusercontent.com/RandomAPI/Randomuser.me-Node/master/api/1.4/data/US/lists/male_first.txt";
    private const string LastNameUrl = "https://raw.githubusercontent.com/RandomAPI/Randomuser.me-Node/master/api/1.4/data/US/lists/last.txt";

    private static List<string> maleFirstNames;
    private static List<string> lastNames;

    private static async Task<List<string>> GetNamesAsync(string apiUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            string response = await httpClient.GetStringAsync(apiUrl);
            return response.Split('\n').Select(name => name.Trim()).ToList();
        }
    }
    */

    private static readonly Random random = new Random();
    private static readonly string[] maleFirstNames = { "Avraham", "Yizchak", "Yaakov", "Moshe", "Yosef", "Aharon", "David", "Dan", "Shimon", "Yehuda" };
    private static readonly string[] femaleFirstNames = { "Sara", "Rivka", "Rachel", "Leah", "Adina", "Chana", "Nahama", "Yehudit", "Nechama", "Elisheva" };
    private static readonly string[] lastNames = { "Cohen", "Levi", "Gold", "Shechter", "Dunner", "Feld", "Miller", "Wilson", "Herth", "Shwarzth" };

    public static void GenerateCSV(string fileName, int rowCount)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        string outputPath = Path.Combine(projectRoot, fileName);
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("FirstName,LastName,Age,Weight,Gender");

            for (int i = 0; i < rowCount; i++)
            {
                string gender = random.Next(2) == 0 ? "Male" : "Female";
                string firstName = (gender == "Male")
                    ? maleFirstNames[random.Next(maleFirstNames.Length)]
                    : femaleFirstNames[random.Next(femaleFirstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                int age = random.Next(18, 70 + 1);
                int weight = random.Next(99, 210 + 1);

                writer.WriteLine($"{firstName},{lastName},{age},{weight},{gender}");
            }
        }
    }

    public static List<Person> ReadCSV(string fileName)
    {
        List<Person> people = new List<Person>();

        try
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string filePath = Path.Combine(projectRoot, fileName);

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                line = reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    string firstName = values[0];
                    string lastName = values[1];
                    int age = int.Parse(values[2]);
                    int weight = int.Parse(values[3]);
                    string gender = values[4];

                    people.Add(new Person(firstName, lastName, age, weight, gender));
                }
            }

            Console.WriteLine($"CSV file '{fileName}' successfully read.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }

        return people;
    }
}
