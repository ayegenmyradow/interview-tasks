using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Core.CQRS.Commands;
using EmployeeManagement.Core.CQRS.Queries;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class PositionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PositionsController> _logger;

        public PositionsController(IMediator mediator, ILogger<PositionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a paged list of positions with optional filtering and sorting
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="searchTerm">Optional search term to filter positions</param>
        /// <param name="sortBy">Property name to sort by</param>
        /// <param name="sortDescending">Sort direction</param>
        /// <returns>A paged list of positions</returns>
        /// <response code="200">Returns the list of positions</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [ProducesResponseType(typeof(GetPositionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPositions(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false)
        {
            _logger.LogInformation("Getting positions. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var query = new GetPositionsQuery(pageNumber, pageSize, searchTerm, sortBy, sortDescending);
            var result = await _mediator.Send(query);

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific position by its ID
        /// </summary>
        /// <param name="id">The ID of the position</param>
        /// <returns>The position details</returns>
        /// <response code="200">Returns the position</response>
        /// <response code="404">If the position is not found</response>
        [HttpGet("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [ProducesResponseType(typeof(PositionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPosition(int id)
        {
            _logger.LogInformation("Getting position with ID: {PositionId}", id);

            var query = new GetPositionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                throw new NotFoundException($"Position with ID {id} not found");
            }

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return Ok(result);
        }

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="command">The position creation details</param>
        /// <returns>The created position</returns>
        /// <response code="201">Returns the newly created position</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(PositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePosition([FromBody] CreatePositionCommand command)
        {
            _logger.LogInformation("Creating new position");

            var position = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
        }

        /// <summary>
        /// Updates an existing position
        /// </summary>
        /// <param name="id">The ID of the position to update</param>
        /// <param name="command">The updated position details</param>
        /// <returns>The updated position</returns>
        /// <response code="200">Returns the updated position</response>
        /// <response code="404">If the position is not found</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="422">If the position has employees and cannot be modified</response>
        [HttpPut("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [ProducesResponseType(typeof(PositionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdatePosition(int id, [FromBody] UpdatePositionCommand command)
        {
            if (id != command.Id)
            {
                throw new ValidationException("Invalid position ID", 
                    new Dictionary<string, string[]> { { "id", new[] { "ID in URL must match ID in body" } } });
            }

            _logger.LogInformation("Updating position with ID: {PositionId}", id);

            var result = await _mediator.Send(command);

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return Ok(result);
        }

        /// <summary>
        /// Deletes a position
        /// </summary>
        /// <param name="id">The ID of the position to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the position was successfully deleted</response>
        /// <response code="404">If the position is not found</response>
        /// <response code="422">If the position has employees and cannot be deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeletePosition(int id)
        {
            _logger.LogInformation("Deleting position with ID: {PositionId}", id);

            await _mediator.Send(new DeletePositionCommand(id));
            return NoContent();
        }
    }
} 