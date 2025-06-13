using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace wiwi.infrastructure.models;

[Table("web_user")]
[Index(nameof(Email), IsUnique = true)]
public class UserModel{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id {get; set;}
  
  [Required]
  [MaxLength(50)]
  [Column("username")]
  public string Username  {get; set;}
  
  [Required]
  [EmailAddress]
  [Column("email")]
  public string Email {get; set;}

  [Required]
  [Column("password")]
  public string HashedPsw{get; set;}
  
  [Required]
  [Column("role")]
  public string Role {get; set;}
  
  [Required]
  [Column("isbanned")]
  public bool Banned {get; set;}
  
  public UserModel(){}

  internal UserModel(string username, string email, string password, string role, bool banned){
    Username = username;
    Email = email;
    HashedPsw = password;
    Role = role;
    Banned = banned;
  }
}
