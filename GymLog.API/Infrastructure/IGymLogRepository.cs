using GymLog.API.Entities;
using System.Linq;

namespace GymLog.API.Infrastructure
{
    public interface IGymLogRepository
    {
        bool SaveAll();

        IQueryable<Muscle> GetMuscles();
        Muscle GetMuscle(int id);
        void Insert(Muscle muscle);
        bool Update(Muscle muscle);
        void Delete(Muscle muscle);

        IQueryable<Equipment> GetEquipments();
        Equipment GetEquipment(int id);
        void Insert(Equipment eq);
        bool Update(Equipment eq);
        void Delete(Equipment eq);

        IQueryable<Exercise> GetExercises();
        IQueryable<Exercise> GetExercisesWithExtras();
        Exercise GetExercise(int id);
        void Insert(Exercise ex);
        bool Update(Exercise ex);
        void Delete(Exercise ex);
    }
}
