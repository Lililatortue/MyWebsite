using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using interfaces.DTO;
using interfaces.command.account;


namespace user.controller;

[ApiController]
[Route("api/account")]
public class AccountController: ControllerBase{
    // no macro action for the moment so I dont need to implement history list to Unexecute
    
    [AllowAnonymous]
    [HttpPost("/create")]
    public async Task<IActionResult> CreateAccount([FromBody]RegisterRequest request){
      UserDTO user = new UserDTO(request.username, request.email, "guest");       
      RegisterCommand command = new RegisterCommand(_context);
      
      var result = await command.Execute(new (user,request.password));
       
      switch(result.errorCode){
        case 200: return Ok(new { message = result.message });
        case 400: return BadRequest(new { message =result.message });
        default : return StatusCode(500,new {message = result.message});
      }
    }


    [Authorize]
    [HttpDelete("/delete/{?}")]
    public async Task<ActionResult> DeleteOwnAccount([FromRoute] String email,
                                                     [FromHeader]String JWT) 
    {
      //email == JWT.email;
      DeleteCommand command = new DeleteCommand(_context, new (email));
      
      var result = await command.Execute();

      switch(result.errorCode){
        case 200: return Ok(new { message = result.message });
        case 400: return BadRequest(new { message = result.message });
        default : return StatusCode(500, new { message = result.message });
      }
    }


  }


//Pour faire les requetes
public record RegisterRequest(String username, String email, String password){};
