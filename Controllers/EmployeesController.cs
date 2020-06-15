using System.Collections.Generic;
using AutoMapper;
using employee_tracker.Data;
using employee_tracker.Dtos;
using employee_tracker.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace employee_tracker.Controllers
{

    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeTrackerRepo _repository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeTrackerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [EnableCors]
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetAllEmployees()
        {
            var employeeItems = _repository.GetAllEmployees();
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employeeItems));
        }

        [EnableCors]
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<EmployeeReadDto> GetEmployeeById(int id)
        {
            var employeeItem = _repository.GetEmployeeById(id);
            if (employeeItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeReadDto>(employeeItem));
        }

        [EnableCors]
        [HttpPost]
        public ActionResult<EmployeeReadDto> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
            _repository.CreateEmployee(employeeModel);
            _repository.SaveChanges();

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

            return CreatedAtRoute(nameof(GetEmployeeById), new { id = employeeReadDto.id }, employeeReadDto);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound(new { message = "Employee not found!", error = true });
            }
            _mapper.Map(employeeUpdateDto, employeeModelFromRepo);
            _repository.UpdateEmployee(employeeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [EnableCors]
        [HttpPatch("{id}")]
        public ActionResult PartialEmployeeUpdate(int id, JsonPatchDocument<EmployeeUpdateDto> patchDoc)
        {
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound(new { message = "Employee not found!", error = true });
            }
            var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(employeeModelFromRepo);
            patchDoc.ApplyTo(employeeToPatch, ModelState);
            if (!TryValidateModel(employeeToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(employeeToPatch, employeeModelFromRepo);
            _repository.UpdateEmployee(employeeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [EnableCors]
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound(new { message = "Employee not found!", error = true });
            }
            _repository.DeleteEmployee(employeeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}