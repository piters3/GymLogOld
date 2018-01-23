using System.Collections.Generic;

namespace GymLog.API.Models
{
    public class ExerciseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MuscleModel Muscle { get; set; }
        public EquipmentModel Equipment { get; set; }
    }
}