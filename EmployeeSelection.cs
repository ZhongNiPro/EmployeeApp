using System.Diagnostics;

namespace EmployeeApp
{
    public static class EmployeeSelection
    {
        public static void GetEmployeesByCriteria()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var dbContext = new DatabaseManagerContext())
            {
                string firstLatterName = "F"; 
                string maleGender = "Male";

                var employees = dbContext.Employees
                    .Where(employee => employee.Gender == maleGender)
                    .ToList();  

               
                var filteredEmployees = employees
                    .Where(employee => employee.Name.ToUpper().StartsWith(firstLatterName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(employee => employee.Name)
                    .ToList();

                stopwatch.Stop();

                Console.WriteLine("Результаты выборки:");

                if (filteredEmployees.Count > 0)
                {
                    foreach (var employee in filteredEmployees)
                    {
                        int age = employee.CalculateAge();
                        Console.WriteLine($"ФИО: {employee.Name}, Дата рождения: {employee.DOB.ToShortDateString()}, Пол: {employee.Gender}, Возраст: {age} лет.");
                    }
                }
                else
                {
                    Console.WriteLine("Нет сотрудников, соответствующих критериям.");
                }

                Console.WriteLine($"Время выполнения запроса: {stopwatch.ElapsedMilliseconds} миллисекунд. {filteredEmployees.Count} записей.");
            }
        }
    }
}
