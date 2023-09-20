using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Tickets;
using TicketTracker.Repositories.Tickets;
using TicketTracker.Repositories.Users;

namespace TicketTracker.Api.Tickets
{
    [ApiController]
    [Route("tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IUsersRepository _usersRepository;

        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketsRepository ticketsRepository, IUsersRepository usersRepository, 
            ILogger<TicketsController> logger)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketsAsync(int? status)
        {
            GetTicketsHandler.Request getTicketsRequest = new GetTicketsHandler.Request { Status = status };
            GetTicketsHandler.Response getTicketsResponse = await new GetTicketsHandler(_ticketsRepository).HandleAsync(getTicketsRequest);

            return Ok(getTicketsResponse.Tickets);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketByIdAsync(int ticketId)
        {
            GetTicketByIdHandler.Request getTicketByIdRequest = new GetTicketByIdHandler.Request { TicketId = ticketId };
            GetTicketByIdHandler.Response getTicketByIdResponse = await new GetTicketByIdHandler(_ticketsRepository).HandleAsync(getTicketByIdRequest);

            return getTicketByIdResponse.Ticket != null ? Ok(getTicketByIdResponse.Ticket) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketHandler.Request request)
        {
            try
            {
                CreateTicketHandler.Response response = await new CreateTicketHandler(_ticketsRepository, _usersRepository).HandleAsync(request);
                return Ok(response.TicketId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{ticketId}/status")]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, [FromBody] ChangeTicketStatusHandler.Request request)
        {
            try
            {
                if (request.TicketId <= 0)
                {
                    request.TicketId = ticketId;
                }

                ChangeTicketStatusHandler.Response response = await new ChangeTicketStatusHandler(_ticketsRepository, _usersRepository).HandleAsync(request);
                
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{ticketId}/assignment")]
        public async Task<IActionResult> UpdateTicketAssignment(int ticketId, [FromBody] ChangeTicketAssignmentHandler.Request request)
        {
            try
            {
                if (request.TicketId <= 0)
                {
                    request.TicketId = ticketId;
                }

                ChangeTicketAssignmentHandler.Response response = await new ChangeTicketAssignmentHandler(_ticketsRepository, _usersRepository).HandleAsync(request);

                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("{ticketId}/notes")]
        public async Task<IActionResult> CreateTicketNote(int ticketId, [FromBody] CreateTicketNoteHandler.Request request)
        {
            try
            {
                if (request.TicketNote.TicketId <= 0)
                {
                    request.TicketNote.TicketId = ticketId;
                }

                CreateTicketNoteHandler.Response response = await new CreateTicketNoteHandler(_ticketsRepository, _usersRepository).HandleAsync(request);

                return Ok(response.TicketNoteId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("{ticketId}/interactions")]
        public async Task<IActionResult> CreateTicketInteraction(int ticketId, [FromBody] CreateTicketInteractionHandler.Request request)
        {
            try
            {
                if (request.TicketInteraction.TicketId <= 0)
                {
                    request.TicketInteraction.TicketId = ticketId;
                }

                CreateTicketInteractionHandler.Response response = await new CreateTicketInteractionHandler(_ticketsRepository, _usersRepository).HandleAsync(request);

                return Ok(response.TicketInteractionId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}