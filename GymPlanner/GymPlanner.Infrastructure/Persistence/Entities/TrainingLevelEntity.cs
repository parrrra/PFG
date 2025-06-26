using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Persistence.Entities
{
    public class TrainingLevelEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }
        public ICollection<ApplicationUserEntity> Users { get; set; } = new List<ApplicationUserEntity>();
    }
}