namespace EmployeeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Не указан режим работы приложения.");
                Console.WriteLine("Использование: myApp <режим>");
                return;
            }

            if (!int.TryParse(args[0], out int mode))
            {
                Console.WriteLine("Некорректный режим. Ожидалось целое число.");
                return;
            }

            switch (mode)
            {
                case 1:
                    break;

                case 2:
                    EmployeeManager.AddEmployeeFromArgs(args);
                    break;

                case 3:
                    EmployeeManager.ViewAllEmployees();
                    break;

                case 4:
                    PackagesCreation.GenerateAndFillEmployees();
                    break;

                case 5:
                    EmployeeSelection.GetEmployeesByCriteria();
                    break;

                default:
                    Console.WriteLine("Неизвестный режим. Попробуйте 1 для создания таблицы или 2 для добавления сотрудника.");
                    break;
            }
        }
    }
}
