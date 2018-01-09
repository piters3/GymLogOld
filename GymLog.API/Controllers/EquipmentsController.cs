using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/equipments")]
    public class EquipmentsController : ApiController
    {

        private IGymLogRepository _repo;


        public EquipmentsController(IGymLogRepository repo)
        {
            _repo = repo;
        }


        [Route("", Name = "Equipments")]
        public IQueryable<EquipmentModel> Get()
        {
            var equipments = _repo.GetEquipments().Select(m =>
                new EquipmentModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                });
            return equipments;
        }


        [Route("{id}", Name = "Equipment")]
        public IHttpActionResult Get(int id)
        {
            var equipment = _repo.GetEquipment(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(new EquipmentModel()
            {
                Id = equipment.Id,
                Name = equipment.Name
            });
        }


        [Route("")]
        public IHttpActionResult Post(Equipment eq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(eq);
            _repo.SaveAll();
            return Ok(eq);
        }


        [Route("{id}")]
        public IHttpActionResult Put(int id, Equipment eq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != eq.Id)
            {
                return BadRequest();
            }
            if (_repo.GetEquipment(id) == null)
            {
                return NotFound();
            }
            _repo.Update(eq);
            _repo.SaveAll();
            return Ok(eq);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Equipment eq = _repo.GetEquipment(id);
            if (eq == null)
            {
                return NotFound();
            }
            _repo.Delete(eq);
            _repo.SaveAll();
            return Ok(eq);
        }
    }
}
