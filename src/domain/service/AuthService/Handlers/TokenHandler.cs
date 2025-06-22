using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using wiwi.domain.service.auth.exception;
using Microsoft.IdentityModel.Tokens;


namespace wiwi.domain.service.auth;

/*
 * JWT Token creation process
 * 
 *To see more about this schema of ValidationLogin go to
 * */
public class TokenHandler: ILoginService{

  private const string TEMP_KEY ="testingtestingtestingtestingtesting123";//for now just a string
  private const string ISSUER   = "http://localhost:5225";
  private const string AUDIENCE = "http://localhost:5225";

  public ILoginService? next{get;set;}

  public void Handle(ref Request request) {
    
    if(request.user is null)
      throw new CredentialsException();

    var claims = new List<Claim>(){
      new Claim(ClaimTypes.Name, request.user.Email),
      new Claim(ClaimTypes.Role, request.user.Username),
    };
    
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TEMP_KEY));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: ISSUER,
        audience: AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: creds
    );

    request.Token = new JwtSecurityTokenHandler().WriteToken(token);
    next?.Handle(ref request);
  }     
}





