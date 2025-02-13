
string file = "mario.csv";
string? choice;

// Exception handling
if (!File.Exists(file))
{
    Console.WriteLine("Error 404: File does not exist");
}
else
{
    // Parallel lists for each column in the csv file
    List<UInt64> ids = [];
    List<string> names = [];
    List<string> descriptions = [];
    List<string> species = [];
    List<string> firstAppearances = [];
    List<string> yearsCreated = [];

    // Read data from file and populate lists
    try
    {
        StreamReader sr = new(file);
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            string[] arr = string.IsNullOrEmpty(line) ? [] : line.Split(',');
            ids.Add(UInt64.Parse(arr[0]));
            names.Add(arr[1]);
            descriptions.Add(arr[2]);
            species.Add(arr[3]);
            firstAppearances.Add(arr[4]);
            yearsCreated.Add(arr[5]);
        }
        sr.Close();
    }
    // Catch exceptions
    catch (Exception e)
    {
        Console.WriteLine("Error: {0}", e.Message);
    }

    // menu options
    do
    {
        Console.WriteLine("1) Display all characters.");
        Console.WriteLine("2) Add a character.");
        Console.WriteLine("Enter any other key to exit.");
        // input response
        choice = Console.ReadLine();

        if (choice == "1")
        {
            // display all characters
            for (int i = 0; i < ids.Count; i++)
            {
                Console.WriteLine("ID: {0}", ids[i]);
                Console.WriteLine("Name: {0}", names[i]);
                Console.WriteLine("Description: {0}", descriptions[i]);
                Console.WriteLine("Species: {0}", species[i]);
                Console.WriteLine("First Appearance: {0}", firstAppearances[i]);
                Console.WriteLine("Year Created: {0}", yearsCreated[i]);
                Console.WriteLine();
            }
        }
        else if (choice == "2")
        {
            // add a character
            UInt64 id = ids.Count > 0 ? ids.Max() + 1 : 1;
            Console.WriteLine("Name?");
            string? name = Console.ReadLine();

            // more exception handling
            if (!string.IsNullOrEmpty(name))
            {
                // .. is the range operator that was suggested by the compiler
                List<string> lowerCaseNames = [.. names.Select(n => n.ToLower())];
                if (lowerCaseNames.Contains(name.ToLower()))
                {
                    Console.WriteLine("Error: Name already exists");
                }
                else
                {
                    Console.WriteLine("Description?");
                    string? desc = Console.ReadLine();
                    Console.WriteLine("Species?");
                    string? spec = Console.ReadLine();
                    Console.WriteLine("First Appearance?");
                    string? fApp = Console.ReadLine();
                    Console.WriteLine("Year Created?");
                    string? yCtd = Console.ReadLine();

                    StreamWriter sw = new(file, append: true);
                    sw.WriteLine("{0},{1},{2},{3},{4},{5}", id, name, desc, spec, fApp, yCtd);
                    sw.Close();

                    ids.Add(id);
                    // ?? is the null coalescing operator I just discovered.
                    // It returns the left-hand operand if the operand is not null; otherwise it returns the right hand operand.
                    names.Add(name ?? "");
                    descriptions.Add(desc ?? "");
                    species.Add(spec ?? "");
                    firstAppearances.Add(fApp ?? "");
                    yearsCreated.Add(yCtd ?? "");

                    Console.WriteLine("{0} added successfully", name);
                }
            }
            else
            {
                Console.WriteLine("Error: Name cannot be empty");
            }
        }

    } while (choice == "1" || choice == "2");
}
