using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private IGymLogRepository _repo;
        private ModelFactory _modelFactory;

        public UsersController(IGymLogRepository repo, ModelFactory modelFactory)
        {
            _repo = repo;
            _modelFactory = modelFactory;
        }

        //[Authorize]
        [Route("", Name = "Users")]
        public IEnumerable<UserModel> Get()
        {
            var users = _repo.GetUsers().ToList().Select(u => _modelFactory.Create(u));
            return users;
        }


        [Route("{id}", Name = "User")]
        public IHttpActionResult Get(string id)
        {
            var user = _repo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_modelFactory.Create(user));
        }


        [Route("")]
        public IHttpActionResult Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Insert(user);
            _repo.SaveAll();
            return Ok(user);
        }


        [Route("{id}")]
        public IHttpActionResult Put(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id)
            {
                return BadRequest();
            }
            if (_repo.GetUser(id) == null)
            {
                return NotFound();
            }
            _repo.Update(user);
            _repo.SaveAll();
            return Ok(user);
        }


        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            User user = _repo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            _repo.Delete(user);
            _repo.SaveAll();
            return Ok(user);
        }
    }
}
