using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Persistence.Entities
{
    public class RoutineDayTrainingTemplateEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(RoutineDay))]
        public Guid RoutineDayId { get; set; }

        public RoutineDayEntity RoutineDay { get; set; } = default!;

        [ForeignKey(nameof(TrainingTemplate))]
        public Guid TrainingTemplateId { get; set; }

        public TrainingTemplateEntity TrainingTemplate { get; set; } = default!;
    }
}