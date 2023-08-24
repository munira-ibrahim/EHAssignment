using EFCoreApi.Models.Dto;
using EFCoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EFCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        protected ResponseDto _response;

        public EmployeeController(IEmployeeService employeeService) { 
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {

            var res = await _employeeService.GetEmployees();
            return Ok(res);
         
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _employeeService.GetEmployeeById(id);
                if (response == null) return NotFound();
                else return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployee(string name) {

            try
            {
                var result = await _employeeService.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // POST api/<EmployeeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            try {
                
                var response = await _employeeService.CreateEmployee(employeeDto);
                if (response == null) return BadRequest();
                else return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<EmployeeController>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                
                if (!ModelState.IsValid) return BadRequest();
                var response = await _employeeService.UpdateEmployee(employeeDto);
                if (response == null) return BadRequest();
                else return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _employeeService.DeleteEmployee(id);

                if (!response) return NotFound();
                else return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
