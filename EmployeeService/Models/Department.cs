namespace Service.Models
{
    public class Department
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }

        private Department(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;
        }

        public static Department Create(string name, string phone)
        {
            return new Department(name, phone);
        }

        public void UpdateName(string name) => Name = name;
        public void UpdatePhone(string phone) => Phone = phone;
    }
}
