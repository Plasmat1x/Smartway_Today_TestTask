namespace Service.Models
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Phone { get; private set; }
        public int CompanyId { get; private set; }

        public Department Department { get; private set; }
        public Passport Passport { get; private set; }

        private Employee(string name, string surname, string phone, int companyId, Department department, Passport passport)
        {
            this.Name = name;
            this.Surname = surname;
            this.Phone = phone;
            this.CompanyId = companyId;
            this.Department = department;
            this.Passport = passport;
        }

        public static Employee Create(string name, string surname, string phone, int companyId, Department department, Passport passport)
        {
            return new Employee(name, surname, phone, companyId, department, passport);
        }

        public void UpdateId(int id) => Id = id;
        public void UpdateName(string name) => Name = name;
        public void UpdateSurname(string surname) => Surname = surname;
        public void UpdatePhone(string phone) => Phone = phone;
        public void UpdateCompanyId(int companyId) => CompanyId = companyId;
        public void UpdateDepartment(Department department) => Department = department;
        public void UpdatePassport(Passport passport) => Passport = passport;
    }
}
