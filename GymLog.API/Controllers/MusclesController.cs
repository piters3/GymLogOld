using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/muscles")]
    public class MusclesController : ApiController
    {
        private IGymLogRepository _repo;
        private ModelFactory _modelFactory;

        public MusclesController(IGymLogRepository repo, ModelFactory modelFactory)
        {
            _repo = repo;
            _modelFactory = modelFactory;
        }

        [Route("", Name = "Muscles")]
        public IEnumerable<MuscleModel> Get()
        {
            var muscles = _repo.GetMuscles().ToList().Select(m => _modelFactory.Create(m));
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
            return Ok(_modelFactory.Create(muscle));
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