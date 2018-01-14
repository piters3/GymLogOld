using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/workouts")]
    public class WorkoutsController : ApiController
    {
        private IGymLogRepository _repo;
        private ModelFactory _modelFactory;

        public WorkoutsController(IGymLogRepository repo, ModelFactory modelFactory)
        {
            _repo = repo;
            _modelFactory = modelFactory;
        }

        [Route("", Name = "Workouts")]
        public IEnumerable<WorkoutModel> Get()
        {
            var workouts = _repo.GetWorkouts().ToList().Select(m => _modelFactory.Create(m));
            return workouts;
        }


        [Route("{id}", Name = "Workout")]
        public IHttpActionResult Get(int id)
        {
            var workout = _repo.GetWorkout(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(_modelFactory.Create(workout));
        }


        [Route("")]
        public IHttpActionResult Post(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(workout);
            _repo.SaveAll();
            return Ok(workout);
        }


        [Route("{id}")]
        public IHttpActionResult Put(int id, Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != workout.Id)
            {
                return BadRequest();
            }
            if (_repo.GetWorkout(id) == null)
            {
                return NotFound();
            }
            _repo.Update(workout);
            _repo.SaveAll();
            return Ok(workout);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Workout workout = _repo.GetWorkout(id);
            if (workout == null)
            {
                return NotFound();
            }
            _repo.Delete(workout);
            _repo.SaveAll();
            return Ok(workout);
        }
    }
}
