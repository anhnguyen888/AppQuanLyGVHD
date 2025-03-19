using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; } = null!;
    
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Role { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
}
