

namespace wiwi.interfaces.DTO;


/*
 * Simple UserDTO psw not included because unsafe
 * has name, email, role as fields
 */
public record UserDTO(string username, string email, string role){
    public string GetUsername()=>username;
    public string GetEmail()=>email;
    public string GetRole()=>role;
}
