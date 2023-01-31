namespace SampleAspNetWithEfCore.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Animal Animal { get; set; }
    }

    public enum Animal
    {
        Cat = 0,
        Dog,
        Parrot,
        Hamster,
    }
}