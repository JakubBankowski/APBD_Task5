using System.ComponentModel.DataAnnotations;

namespace APBD_Task5.Models;

public class Reservation
{
    public int Id { get; set; }
    
    [Required]
    public int RoomId { get; set; }
    
    [Required]
    [MinLength(1)]
    public string OrganizerName { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Topic { get; set; } = string.Empty;
    
    [Required]
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Status { get; set; } = string.Empty;
}