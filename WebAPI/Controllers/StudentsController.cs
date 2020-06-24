using DAL.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private DbStudentService Service { get; } = new DbStudentService();

        public IHttpActionResult Get()
        {
            return Ok(Service.Read());
        }

        public IHttpActionResult Get(string lastName)
        {
            return Ok(Service.Read(lastName));
        }

        [Route("api/students/{id}", Name = "GetStudent")]
        public IHttpActionResult Get(int id)
        {
            var student = Service.Read(id);
            if (student == null)
                return NotFound();

            return Ok(Service.Read(id));
        }

        public IHttpActionResult Post(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = Service.Create(student);
            return CreatedAtRoute("GetStudent", new { id = id }, id);
        }

        [HttpPut]
        [Route("api/students/{id}")]
        public IHttpActionResult Put(int id, Student student)
        {
            if (Service.Read(id) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Service.Update(id, student);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            var student = Service.Read(id);
            if (student == null)
                return NotFound();

            Service.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
