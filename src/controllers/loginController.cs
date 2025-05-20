using loginService;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/login")]
public class loginController {
  //might create a factory methode to pre-create standards version
  private readonly BanCheckHandler _ban;
  private readonly AuthHandler _auth;
  private readonly TokenHandler _token; 
  
  public loginController(){
    _ban =new BanCheckHandler(new BanService());
    _auth =new AuthHandler(new UserService());
    _token =new TokenHandler();
  }
  //routes:

  //POST: login/login
  [HttpPost]
  public IActionResult login([FromBody]string name,[FromBody] string psw){
      Request request = new Request(name, psw);

      _ban.SetNext(_auth);
      _auth.SetNext(_token);     
      _ban.Handle(ref request);//modifies request
      
     return Ok(request.Token);

  }

}
