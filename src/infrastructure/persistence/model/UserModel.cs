using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace wiwi.infrastructure.models;

[Table("web_user")]
[Index(nameof(Email), IsUnique = true)]
public class UserModel{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int id {get; set;}
  
  [Required]
  [MaxLength(50)]
  [Column("username")]
  public required string Username  {get; set;}
  
  [Required]
  [EmailAddress]
  [Column("email")]
  public required string Email {get; set;}

  [Required]
  [Column("password")]
  public required string HashedPsw{get; set;}
  
  [Required]
  [Column("role")]
  public required string Role {get; set;}
  
  [Required]
  [Column("isbanned")]
  public required bool Banned {get; set;}

  

  [SetsRequiredMembers] 
  public UserModel(string username, string email, string hashedPsw, string role, bool banned){
    Username = username;
    Email = email;
    HashedPsw = hashedPsw;
    Role = role;
    Banned = banned;
  }
}
