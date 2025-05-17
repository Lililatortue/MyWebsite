using System.Identm.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens;




namespace loginService;

/*
 * JWT Token creation process
 * 
 *To see more about this schema of ValidationLogin go to
 * */
public class TokenHandler: LoginHandler{
  //for now will create env settings later
  private const string secretkey = "testing";

  private readonly SymmetricSecurityKey _key;

  public override void Handle(ref Request request) {
     _key
    
  }
  
  public CreateToken(ref Request request){
    
  }
  
     
}





