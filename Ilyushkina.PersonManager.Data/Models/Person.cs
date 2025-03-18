namespace Ilyushkina.PersonManager.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }
    }
}
