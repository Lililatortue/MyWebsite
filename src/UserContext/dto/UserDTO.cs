



namespace DTO;


/*
 * Simple UserDTO psw not included because unsafe
 * has name, email, role as fields
 */
public class UserDTO(string name, string email, string role){
    public string GetUsername()=>name;
    public string GetEmail()=>email;
    public string GetRole()=>role;

}
