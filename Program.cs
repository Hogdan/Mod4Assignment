
string file = "mario.csv";
string? choice;

if (!File.Exists(file))
{
    Console.WriteLine("Error 404: File does not exist");

}
else
{
    List<UInt64> ids = [];
    List<string> names = [];
    List<string> descriptions = [];
    List<string> species = [];
    List<string> firstAppearances = [];
    List<string> yearsCreated = [];
    try {
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
    catch (Exception e)
    {
        Console.WriteLine("Error: {0}", e.Message);
    }

    do
    {
        Console.WriteLine("1) Display all characters.");
        Console.WriteLine("2) Add a character.");
        Console.WriteLine("Enter any other key to exit.");
        // input response
        choice = Console.ReadLine();

        if (choice == "1")
        {
            // read data from file
            if (File.Exists(file))
            {
                // accumulator needed for count
                int count = 0;
                // read data from file
                StreamReader sr = new(file);
                // read the header
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    // convert string to array
                    string[] arr = string.IsNullOrEmpty(line) ? [] : line.Split(',');
                    // display array data
                    Console.WriteLine("ID: {0}", arr[0]);
                    Console.WriteLine("Name: {0}", arr[1]);
                    Console.WriteLine("Description: {0}", arr[2]);
                    Console.WriteLine("Species: {0}", arr[3]);
                    Console.WriteLine("First Appearance: {0}", arr[4]);
                    Console.WriteLine("Year Created: {0}\n", arr[5]);
                    // add to accumulator
                    count += 1;
                }
                sr.Close();
                Console.WriteLine("{0} Characters saved to file", count);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        else if (choice == "2")
        {
            UInt64 id = ids.Count > 0 ? ids.Max() + 1 : 1;
            Console.WriteLine("Name?");
            string? name = Console.ReadLine();
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

            Console.WriteLine("Character added successfully");
        }
    } while (choice == "1" || choice == "2");
}
