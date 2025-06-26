using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Application.TrainingLevel.DTOs
{
    public class TrainingLevelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}