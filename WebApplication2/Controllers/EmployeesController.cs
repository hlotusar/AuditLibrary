using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var service = new EmployeeService();
            return service.GetAll();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public Employee Post([FromBody] DtoEmployee dto)
        {
            var service=new EmployeeService();
            return service.Post(dto);

        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public Employee Put(int id, [FromBody] DtoEmployee dto)
        {
            var service = new EmployeeService();
            return service.Put(id, dto);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public Employee Delete(int id)
        {
            var service = new EmployeeService();
            return service.Delete(id);
        }
    }
}
