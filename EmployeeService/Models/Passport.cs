namespace Service.Models
{
    public class Passport
    {
        public string Type { get; private set; }
        public string Number { get; private set; }

        private Passport(string type, string number)
        {
            this.Type = type;
            this.Number = number;
        }

        public static Passport Create(string type, string number)
        {
            return new Passport(type, number);
        }

        public void UpdateType(string type) => Type = type;
        public void UpdateNumber(string number) => Number = number;
    }
}
