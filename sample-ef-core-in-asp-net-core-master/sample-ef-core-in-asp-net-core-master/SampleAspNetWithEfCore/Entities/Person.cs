namespace SampleAspNetWithEfCore.Entities
{
    public class Person
    {
        public Team Team { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public Pet Pet { get; set; }

        public bool IsArchived { get; set; }
    }
}
