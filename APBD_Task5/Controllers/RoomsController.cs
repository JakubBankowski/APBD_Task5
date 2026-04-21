
using APBD_Task5.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Room>> GetAll(
        [FromQuery]int? minCapacity,
        [FromQuery]bool? hasProjector,
        [FromQuery]bool? activateOnly
        )
    {
        var rooms = DataStore.Rooms.AsEnumerable();
        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
        }

        if (activateOnly.HasValue && activateOnly.Value)
        {
            rooms = rooms.Where(r => r.IsActive);
        }
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

    [HttpPost]
    public ActionResult<Room> CreateRoom(Room room)
    {
        room.Id = DataStore.NextRoomId;
        DataStore.Rooms.Add(room);
        
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<List<Room>> GetByBuildingCode(string buildingCode)
    {
        var rooms = DataStore.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Ok(rooms);
    }
    
    [HttpPut("{id:int}")]
    public ActionResult UpdateRoom(int id, [FromBody] Room room)
    {
        var index = DataStore.Rooms.FindIndex(r => r.Id == id);
        if (index == -1)
        {
            return NotFound($"Room with id {id} not found.");
        }
        room.Id = id;
        DataStore.Rooms[index] = room;

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult DeleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound($"Room with id {id} not found.");
        }
        DataStore.Rooms.Remove(room);
        return NoContent();
    }
}