

namespace UserModel;

public class UserDTO(string username, string email, string role){
  
  public string Username() => username;
  public string Email() => email;
  public string Role() => role;
}
