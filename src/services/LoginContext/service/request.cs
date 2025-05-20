using UserModel;


namespace loginService;
/*No logic for now just a plain POD
 */
public struct Request{
    public string username;
    public string password;

    public UserDTO user;
    public string Token;
    
}
