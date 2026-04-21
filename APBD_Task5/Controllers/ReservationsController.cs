using APBD_Task5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Reservation>> GetAll(
        [FromQuery]DateOnly? date,
        [FromQuery]string? status,
        [FromQuery]int? roomId
        )
    {
        var reservations = DataStore.Reservations.AsEnumerable();

        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.Date == date);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomId == roomId);
        }
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

    [HttpPost]
    public ActionResult<Reservation> CreateReservation([FromBody]Reservation reservation)
    {
        var roomExists = DataStore.Rooms.Any(r => r.Id == reservation.Id);
        if (!roomExists) return BadRequest($"Room with id {reservation.RoomId} does not exist.");
        reservation.Id = DataStore.NextReservationId;
        DataStore.Reservations.Add(reservation);
        
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }
    
    [HttpPut("{id:int}")]
    public ActionResult UpdateReservation(int id, [FromBody] Reservation reservation)
    {
        var index = DataStore.Reservations.FindIndex(r => r.Id == id);

        if (index == -1)
        {
            return NotFound($"Reservation with id {id} not found.");
        }

        var roomExists = DataStore.Rooms.Any(r => r.Id == reservation.RoomId);
        if (!roomExists)
        {
            return BadRequest($"Room with id {reservation.RoomId} does not exist.");
        }

        reservation.Id = id;
        DataStore.Reservations[index] = reservation;

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult DeleteReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }

        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}