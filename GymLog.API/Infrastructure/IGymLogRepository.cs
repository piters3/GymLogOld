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

        IQueryable<User> GetUsers();
        User GetUser(string id);
        void Insert(User user);
        bool Update(User user);
        void Delete(User user);

        IQueryable<Workout> GetWorkouts();
        Workout GetWorkout(int id);
        void Insert(Workout wk);
        bool Update(Workout wk);
        void Delete(Workout wk);

        IQueryable<Daylog> GetDaylogs();
        Daylog GetDaylog(int id);
        void Insert(Daylog dl);
        bool Update(Daylog dl);
        void Delete(Daylog dl);
    }
}
