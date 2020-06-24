using DAL.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private DbStudentService Service { get; } = new DbStudentService();

        public async Task<IHttpActionResult> Get()
        {
            return Ok(await Service.ReadAsync());
        }

        public async Task<IHttpActionResult> Get(string lastName)
        {
            return Ok(Service.ReadAsync(lastName));
        }

        [Route("api/students/{id}", Name = "GetStudent")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var student = await Service.ReadAsync(id);
            if (student == null)
                return NotFound();

            return Ok(await Service.ReadAsync(id));
        }

        public async Task<IHttpActionResult> Post(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id =await  Service.CreateAsync(student);
            return CreatedAtRoute("GetStudent", new { id = id }, id);
        }

        [HttpPut]
        [Route("api/students/{id}")]
        public async Task<IHttpActionResult> Put(int id, Student student)
        {
            if (await Service.ReadAsync(id) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Task.Delay(10000);


            await Service.UpdateAsync(id, student);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var student = await Service.ReadAsync(id);
            if (student == null)
                return NotFound();

            await Service.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
