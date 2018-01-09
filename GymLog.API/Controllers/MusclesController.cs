using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/muscles")]
    public class MusclesController : ApiController
    {

        private IGymLogRepository _repo;

        public MusclesController(IGymLogRepository repo)
        {
            _repo = repo;
        }

        //[Authorize]
        [Route("", Name = "Muscles")]
        public IQueryable<MuscleModel> Get()
        {
            var muscles = _repo.GetMuscles().Select(m =>
                new MuscleModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                });
            return muscles;
        }


        [Route("{id}", Name = "Muscle")]
        public IHttpActionResult Get(int id)
        {
            var muscle = _repo.GetMuscle(id);
            if (muscle == null)
            {
                return NotFound();
            }
            return Ok(new MuscleModel()
            {
                Id = muscle.Id,
                Name = muscle.Name
            });
        }


        [Route("")]
        public IHttpActionResult Post(Muscle muscle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(muscle);
            _repo.SaveAll();
            return Ok(muscle);
        }


        [Route("{id}")]
        public IHttpActionResult Put(int id, Muscle muscle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != muscle.Id)
            {
                return BadRequest();
            }
            if (_repo.GetMuscle(id) == null)
            {
                return NotFound();
            }
            _repo.Update(muscle);
            _repo.SaveAll();
            return Ok(muscle);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Muscle muscle = _repo.GetMuscle(id);
            if (muscle == null)
            {
                return NotFound();
            }
            _repo.Delete(muscle);
            _repo.SaveAll();
            return Ok(muscle);
        }
    }
}