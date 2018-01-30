using GymLog.API.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace GymLog.API.Infrastructure
{
    public class GymLogRepository : IGymLogRepository, IDisposable
    {
        private GymLogContext _ctx;

        public GymLogRepository(GymLogContext ctx)
        {
            _ctx = ctx;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IQueryable<Muscle> GetMuscles()
        {
            return _ctx.Muscles;
        }

        public Muscle GetMuscle(int id)
        {
            return _ctx.Muscles.Where(m => m.Id == id).FirstOrDefault();
        }

        public void Insert(Muscle muscle)
        {
            _ctx.Muscles.Add(muscle);
        }

        public bool Update(Muscle muscle)
        {
            var existing = _ctx.Muscles.FirstOrDefault(m => m.Id == muscle.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Muscles.Attach(muscle);
            _ctx.Entry(muscle).State = EntityState.Modified;
            return true;
        }

        public void Delete(Muscle muscle)
        {
            _ctx.Muscles.Remove(muscle);
        }

        public IQueryable<Equipment> GetEquipments()
        {
            return _ctx.Equipments;
        }

        public Equipment GetEquipment(int id)
        {
            return _ctx.Equipments.Where(e => e.Id == id).FirstOrDefault();
        }

        public void Insert(Equipment eq)
        {
            _ctx.Equipments.Add(eq);
        }

        public bool Update(Equipment eq)
        {
            var existing = _ctx.Equipments.FirstOrDefault(e => e.Id == eq.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Equipments.Attach(eq);
            _ctx.Entry(eq).State = EntityState.Modified;
            return true;
        }

        public void Delete(Equipment eq)
        {
            _ctx.Equipments.Remove(eq);
        }

        public IQueryable<Exercise> GetExercises()
        {
            return _ctx.Exercises;
        }

        public IQueryable<Exercise> GetExercisesWithExtras()
        {
            return _ctx.Exercises.Include("Muscles").Include("Equipments");
        }

        public void Insert(Exercise ex)
        {
            _ctx.Exercises.Add(ex);
        }

        public Exercise GetExercise(int id)
        {
            return _ctx.Exercises.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Update(Exercise ex)
        {
            var existing = _ctx.Exercises.FirstOrDefault(e => e.Id == ex.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Exercises.Attach(ex);
            _ctx.Entry(ex).State = EntityState.Modified;
            return true;
        }

        public void Delete(Exercise ex)
        {
            _ctx.Exercises.Remove(ex);
        }


        public IQueryable<User> GetUsers()
        {
            return _ctx.Users;
        }

        public User GetUser(string id)
        {
            return _ctx.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public void Insert(User user)
        {
            _ctx.Users.Add(user);
        }

        public bool Update(User user)
        {
            var existing = _ctx.Users.FirstOrDefault(e => e.Id == user.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Users.Attach(user);
            _ctx.Entry(user).State = EntityState.Modified;
            return true;
        }

        public void Delete(User user)
        {
            _ctx.Users.Remove(user);
        }


        public IQueryable<Workout> GetWorkouts()
        {
            return _ctx.Workouts;
        }

        public Workout GetWorkout(int id)
        {
            return _ctx.Workouts.Where(w => w.Id == id).FirstOrDefault();
        }

        public void Insert(Workout wk)
        {
            _ctx.Workouts.Add(wk);
        }

        public bool Update(Workout wk)
        {
            var existing = _ctx.Workouts.FirstOrDefault(e => e.Id == wk.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Workouts.Attach(wk);
            _ctx.Entry(wk).State = EntityState.Modified;
            return true;
        }

        public void Delete(Workout wk)
        {
            _ctx.Workouts.Remove(wk);
        }


        public IQueryable<Daylog> GetDaylogs()
        {
            return _ctx.Daylogs;
        }

        public Daylog GetDaylog(int id)
        {
            return _ctx.Daylogs.Where(d => d.Id == id).FirstOrDefault();
        }

        public void Insert(Daylog dl)
        {
            _ctx.Daylogs.Add(dl);
        }

        public bool Update(Daylog dl)
        {
            var existing = _ctx.Daylogs.FirstOrDefault(e => e.Id == dl.Id);
            if (existing == null)
            {
                return false;
            }
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Daylogs.Attach(dl);
            _ctx.Entry(dl).State = EntityState.Modified;
            return true;
        }

        public void Delete(Daylog dl)
        {
            _ctx.Daylogs.Remove(dl);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ctx != null)
                {
                    _ctx.Dispose();
                    _ctx = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<Workout> GetUserWorkouts(string id)
        {
            return _ctx.Workouts.Where(w => w.UserId == id);
        }
    }
}