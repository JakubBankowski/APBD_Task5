
using APBD_Task5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Room>> GetAll()
    {
        var rooms = DataStore.Rooms.AsEnumerable();
        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound($"Room with id {id} not found.");
        }
        return Ok(room);
    }
}