using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/exercises")]
    public class ExercisesController : ApiController
    {
        private IGymLogRepository _repo;

        public ExercisesController(IGymLogRepository repo)
        {
            _repo = repo;
        }


        [Route("", Name = "Exercises")]
        public IQueryable<ExerciseModel> Get()
        {
            var exercises = _repo.GetExercises().Select(e =>
                new ExerciseModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Equipment = e.Equipment.Name,
                    Muscle = e.Muscle.Name
                });
            return exercises;
        }


        [Route("{id}", Name = "Exercise")]
        public IHttpActionResult Get(int id)
        {
            var ex = _repo.GetExercise(id);
            if (ex == null)
            {
                return NotFound();
            }
            return Ok(new ExerciseModel()
            {
                Id = ex.Id,
                Name = ex.Name,
                Description = ex.Description,
                Equipment = ex.Equipment.Name,
                Muscle = ex.Muscle.Name
            });
        }


        [Route("")]
        public IHttpActionResult Post(Exercise ex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(ex);
            _repo.SaveAll();
            return Ok(ex);
        }


        [Route("{id}")]
        public IHttpActionResult Put(int id, Exercise ex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != ex.Id)
            {
                return BadRequest();
            }
            if (_repo.GetExercise(id) == null)
            {
                return NotFound();
            }
            _repo.Update(ex);
            _repo.SaveAll();
            return Ok(ex);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Exercise ex = _repo.GetExercise(id);
            if (ex == null)
            {
                return NotFound();
            }
            _repo.Delete(ex);
            _repo.SaveAll();
            return Ok(ex);
        }
    }
}
