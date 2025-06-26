using System;

namespace GymPlanner.Application.BodyParts.Commands.UpdateBodyPart
{
    public class UpdateBodyPartCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
