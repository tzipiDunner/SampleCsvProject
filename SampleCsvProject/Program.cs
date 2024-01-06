CSV.GenerateCSV("people.csv", 1000);

try
{
    List<Person> people = CSV.ReadCSV("people.csv");

    //average age of all people
    double averageAge = people.Average(person => person.Age);
    Console.WriteLine($"Average age of all people: {averageAge} years");

    // Find the total number of people weighing between 120lbs and 140lbs
    int peopleInRangeCount = people.Count(person => person.Weight >= 120 && person.Weight <= 140);
    Console.WriteLine($"Total number of people weighing between 120lbs and 140lbs: {peopleInRangeCount}");

    // Find the average age of people weighing between 120lbs and 140lbs
    double averageAgeInRange = people
        .Where(person => person.Weight >= 120 && person.Weight <= 140)
        .Average(person => person.Age);
    Console.WriteLine($"Average age of people weighing between 120lbs and 140lbs: {averageAgeInRange} years");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}