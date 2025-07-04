using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Core.Exceptions;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a paged list of employees with optional filtering and sorting
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="searchTerm">Optional search term to filter employees</param>
        /// <param name="sortBy">Property name to sort by</param>
        /// <param name="sortDescending">Sort direction</param>
        /// <returns>A paged list of employees</returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache for 1 minute
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployees(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false)
        {
            _logger.LogInformation("Getting employees. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var query = new GetEmployeesQuery(pageNumber, pageSize, searchTerm, sortBy, sortDescending);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific employee by their ID
        /// </summary>
        /// <param name="id">The ID of the employee</param>
        /// <returns>The employee details</returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="404">If the employee is not found</response>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            _logger.LogInformation("Getting employee with ID: {EmployeeId}", id);

            var query = new GetEmployeeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                throw new NotFoundException($"Employee with ID {id} not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="command">The employee creation details</param>
        /// <returns>The created employee</returns>
        /// <response code="201">Returns the newly created employee</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            _logger.LogInformation("Creating new employee");

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing employee
        /// </summary>
        /// <param name="id">The ID of the employee to update</param>
        /// <param name="command">The updated employee details</param>
        /// <returns>The updated employee</returns>
        /// <response code="200">Returns the updated employee</response>
        /// <response code="404">If the employee is not found</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                throw new ValidationException("Invalid employee ID", 
                    new Dictionary<string, string[]> { { "id", new[] { "ID in URL must match ID in body" } } });
            }

            _logger.LogInformation("Updating employee with ID: {EmployeeId}", id);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Deletes an employee
        /// </summary>
        /// <param name="id">The ID of the employee to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the employee was successfully deleted</response>
        /// <response code="404">If the employee is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation("Deleting employee with ID: {EmployeeId}", id);

            await _mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }
    }
} 