using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using interfaces.DTO;
using interfaces.command.user;

namespace interfaces.controller;


[ApiController]
[Route("api/auth")]
public class AuthController{
  private readonly AuthService _authService;
  
  [AllowAnonymous] 
  [HttpPost("/login")]
  public ActionResult<IActionResult> login([FromBody]LoginRequest request){
  
    var result = _authService;

    switch(result.errorCode) {
      case 200: return Ok(new { result.message });
      case 400: return BadRequest(new { result.message });
      default : return StatusCode(500, new { result.message});
    }
  }
}


public record LoginRequest(string email, string password);
