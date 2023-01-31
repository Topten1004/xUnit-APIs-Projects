namespace SampleAspNetWithEfCore.Entities
{
    public class Team
    {
        public const string DefaultTeamCode = "default";

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}