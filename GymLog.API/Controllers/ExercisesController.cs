﻿using GymLog.API.Entities;
using GymLog.API.Infrastructure;
using GymLog.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GymLog.API.Controllers
{
    [RoutePrefix("api/exercises")]
    public class ExercisesController : ApiController
    {
        private IGymLogRepository _repo;
        private ModelFactory _modelFactory;

        public ExercisesController(IGymLogRepository repo, ModelFactory modelFactory)
        {
            _repo = repo;
            _modelFactory = modelFactory;
        }


        [Route("", Name = "Exercises")]
        public IEnumerable<ExerciseModel> Get()
        {
            var exercises = _repo.GetExercises().ToList().Select(m => _modelFactory.Create(m));
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
            return Ok(_modelFactory.Create(ex));
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
