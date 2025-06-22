using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wiwi.interfaces.command.auth;
using wiwi.interfaces.command;

namespace interfaces.controller.auth;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase{
  private readonly IServiceProvider _service; 
  
  public AuthController(IServiceProvider service){
    _service = service;
  }



  [AllowAnonymous] 
  [HttpPost("/login")]
  public async Task<IActionResult> login([FromBody]TLoginAction request){
    var command = _service.GetRequiredService<ICommand<TLoginAction>>();
    if(command is null)
      throw new ArgumentException("action doesnt exist");


    var result  = await command.Execute(request);

    if(result.message is null)
      throw new ArgumentException("invalid token");

    switch(result.errorCode) {
      case 200: {
        Response.Cookies.Append("auth_token", result.message , new CookieOptions {
          HttpOnly = true,
          Secure = false, //for testing
          SameSite = SameSiteMode.Strict,
          Expires = DateTimeOffset.UtcNow.AddHours(1)
        });

        return Ok("succesful login");
      }
      case 400: return BadRequest(new { result.message });
      default : return StatusCode(500, new { result.message});
    }
  }
}



