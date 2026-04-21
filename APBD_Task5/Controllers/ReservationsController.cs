using APBD_Task5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Reservation>> GetAll()
    {
        var reservations = DataStore.Reservations.AsEnumerable();
        return Ok(reservations.ToList());
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }
        return Ok(reservation);
    }
}