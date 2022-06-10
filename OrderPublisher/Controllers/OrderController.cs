using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;
using System;
using System.Threading.Tasks;

namespace OrderPublisher.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBus _bus;
        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("api/Order")]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if(ticket != null)
            {
                ticket.Booked = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/orderTicketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                return Ok();
            }
            return BadRequest();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
