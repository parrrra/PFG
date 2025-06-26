namespace GymPlanner.Domain.Entities
{
    public class BodyPart
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public BodyPart(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public BodyPart(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}