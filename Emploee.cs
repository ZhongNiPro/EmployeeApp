namespace EmployeeApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DOB { get; set; }
        public string Gender { get; set; } = null!;

        public Employee() { }

        public Employee(string name, DateTime dob, string gender)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DOB = dob;
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DOB.Year;
            if (DOB.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
