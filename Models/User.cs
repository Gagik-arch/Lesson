
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }
}
