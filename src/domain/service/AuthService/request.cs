using wiwi.domain.entities;


namespace wiwi.domain.service.auth;
/*No logic for now just a plain POD
 * */ 
public struct Request{
    public string email;
    public string password;

    internal User? user;
    public string? Token;
    
}
