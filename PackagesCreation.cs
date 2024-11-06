namespace EmployeeApp
{
    public class PackagesCreation
    {
        private static Random s_random = new Random();

        private static readonly string femaleNamesFile = "Resources/Russian_female_trans_names.txt";
        private static readonly string maleNamesFile = "Resources/Russian_male_trans_names.txt";
        private static readonly string lastNamesFile = "Resources/Russian_trans_surnames.txt";

        public static void GenerateAndFillEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var femaleNames = File.ReadAllLines(femaleNamesFile).ToList();
            var maleNames = File.ReadAllLines(maleNamesFile).ToList();
            var lastNames = File.ReadAllLines(lastNamesFile).ToList();

            string maleGender = "Male";
            string femaleGender = "Female";
            int limitRecords = 1000000;

            for (int i = 0; i < limitRecords; i++)
            {
                string gender = s_random.Next(2) == 0 ? maleGender : femaleGender;
                string firstName = gender == femaleGender ? CreateRandomName(femaleNames) : CreateRandomName(maleNames);
                string lastName = CreateRandomLastName(lastNames);
                DateTime dob = CreateRandomDateOfBirth();

                Employee employee = new Employee($"{lastName} {firstName}", dob, gender);
                employees.Add(employee);
            }

            int limitSpecialRecords = 100;

            for (int i = 0; i < limitSpecialRecords; i++)
            {
                string lastName = CreateRandomLastNameStartingWithF(lastNames);
                var employee = new Employee(lastName, CreateRandomDateOfBirth(), maleGender);
                employees.Add(employee);
            }

            SendEmployeesToDatabase(employees);
        }

        private static void SendEmployeesToDatabase(List<Employee> employees)
        {
            using (DatabaseManagerContext dbContext = new DatabaseManagerContext())
            {
                dbContext.Employees.AddRange(employees);
                dbContext.SaveChanges();
            }

            Console.WriteLine($"{employees.Count} сотрудников успешно добавлено в базу данных.");
        }

        private static string CreateRandomName(List<string> names)
        {
            return names[s_random.Next(names.Count)];
        }

        private static string CreateRandomLastName(List<string> lastNames)
        {
            return lastNames[s_random.Next(lastNames.Count)];
        }

        private static string CreateRandomLastNameStartingWithF(List<string> lastNames)
        {
            string firstLatterName = "F";
            var filteredNames = lastNames.Where(name => name.StartsWith(firstLatterName, StringComparison.OrdinalIgnoreCase)).ToList();
            return filteredNames[s_random.Next(filteredNames.Count)];
        }

        private static DateTime CreateRandomDateOfBirth()
        {
            DateTime start = new DateTime(1970, 1, 1);
            DateTime end = new DateTime(2000, 12, 31);
            var range = end - start;
            DateTime dob = DateTime.SpecifyKind(start.AddDays(s_random.Next((int)range.TotalDays)), DateTimeKind.Utc);

            return dob;
        }
    }
}
