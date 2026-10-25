using Application.Dtos;
using Application.Ports;
using Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController(IGuestManager guestManager) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateGuest([FromBody] GuestDto request)
        {
            var result = await guestManager.CreateGuest(new CreateGuestRequest { Data = request });

            if (result.Success) return Created("", result.Data);

            return BadRequest(result);
        }
    }
}
