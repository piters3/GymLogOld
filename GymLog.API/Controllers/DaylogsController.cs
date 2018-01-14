using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/daylogs")]
    public class DaylogsController : ApiController
    {
        private IGymLogRepository _repo;
        private ModelFactory _modelFactory;

        public DaylogsController(IGymLogRepository repo, ModelFactory modelFactory)
        {
            _repo = repo;
            _modelFactory = modelFactory;
        }

        [Route("", Name = "Daylogs")]
        public IEnumerable<DaylogModel> Get()
        {
            var daylogs = _repo.GetDaylogs().ToList().Select(m => _modelFactory.Create(m));
            return daylogs;
        }


        [Route("{id}", Name = "Daylog")]
        public IHttpActionResult Get(int id)
        {
            var dl = _repo.GetDaylog(id);
            if (dl == null)
            {
                return NotFound();
            }
            return Ok(_modelFactory.Create(dl));
        }


        [Route("")]
        public IHttpActionResult Post(Daylog dl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(dl);
            _repo.SaveAll();
            return Ok(dl);
        }


        [Route("{id}")]
        public IHttpActionResult Put(int id, Daylog dl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != dl.Id)
            {
                return BadRequest();
            }
            if (_repo.GetDaylog(id) == null)
            {
                return NotFound();
            }
            _repo.Update(dl);
            _repo.SaveAll();
            return Ok(dl);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Daylog dl = _repo.GetDaylog(id);
            if (dl == null)
            {
                return NotFound();
            }
            _repo.Delete(dl);
            _repo.SaveAll();
            return Ok(dl);
        }
    }
}
