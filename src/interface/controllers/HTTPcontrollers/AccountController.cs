using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wiwi.interfaces.command;
using wiwi.interfaces.command.account;
namespace interfaces.controller.account;

[ApiController]
[Route("api/account")]
public class AccountController: ControllerBase {
    // no macro action for the moment so I dont need to implement history list to Unexecute
    private readonly IServiceProvider _provider;

    public AccountController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [AllowAnonymous]
    [HttpPost("/create")]
    public async Task<IActionResult> CreateAccount([FromBody]TRegisterAction request)
    { 
     var command = _provider.GetService<ICommand<TRegisterAction>>(); 
     if(command is null)
       throw new ArgumentException("Invalid argument");

     var result = await command.Execute(request); 
         
      switch(result.errorCode) {
        case 200: return Ok(new { message = result.message });
        case 400: return BadRequest(new { message =result.message });
        default : return StatusCode(500,new {message = result.message});
      }
    }


    [Authorize]
    [HttpDelete("/delete")]
    public async Task<ActionResult> DeleteOwnAccount(
                                      [FromHeader]TDeleteAccountAction request)
    { 
      var command = _provider.GetService<ICommand<TDeleteAccountAction>>();
      if(command is null)
        throw new ArgumentException("Invalid argument");

      var result = await command.Execute(request);

      switch(result.errorCode) {
        case 200: return Ok(new { message = result.message });
        case 400: return BadRequest(new { message = result.message });
        default : return StatusCode(500, new { message = result.message });
      }
    }
}




