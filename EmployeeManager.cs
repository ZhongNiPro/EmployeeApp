using System.Globalization;

namespace EmployeeApp
{
    public static class EmployeeManager
    {
        public static void AddEmployeeFromArgs(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Недостаточно данных. Использование: myApp 2 \"ФИО\" ДатаРождения Пол");
                return;
            }

            string nameArg = args[1];
            string dobArg = args[2];
            string genderArg = args[3];

            if (!DateTime.TryParseExact(dobArg, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
            {
                Console.WriteLine("Неверный формат даты. Ожидается формат yyyy-MM-dd.");
                return;
            }

            dob = DateTime.SpecifyKind(dob, DateTimeKind.Utc);

            AddEmployee(nameArg, dob, genderArg);
        }

        public static void AddEmployee(string name, DateTime dob, string gender)
        {
            Employee employee = new Employee(name, dob, gender);

            using (DatabaseManagerContext database = new DatabaseManagerContext())
            {
                database.Employees.Add(employee);
                database.SaveChanges();
                Console.WriteLine($"Сотрудник {employee.Name} добавлен в базу данных. Возраст: {employee.CalculateAge()} лет.");
            }
        }

        public static void ViewAllEmployees()
        {
            using (DatabaseManagerContext dbContext = new DatabaseManagerContext())
            {
                var employees = dbContext.Employees
                    .Distinct()
                    .OrderBy(employee => employee.Name) 
                    .ToList();

                Console.WriteLine("Список сотрудников (с уникальным значением ФИО и даты рождения):");

                foreach (var employee in employees)
                {
                    int age = employee.CalculateAge();
                    Console.WriteLine($"ФИО: {employee.Name}, Дата рождения: {employee.DOB.ToShortDateString()}, Пол: {employee.Gender}, Возраст: {age} лет.");
                }
            }
        }              
    }
}
