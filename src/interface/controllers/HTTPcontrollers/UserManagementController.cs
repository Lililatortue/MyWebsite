using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using wiwi.interfaces.patterns.mediator;

namespace interfaces.controller;


[ApiController]
[Authorize(Roles="admin")]
[Route("api/user/")]
public class UserManagmentController: ControllerBase{
    private readonly CommandMediator<Object> _mediator;  
    
    public UserManagmentController(CommandMediator<Object> mediator){
      _mediator = mediator;
    }
    

    [HttpPatch("/roles")]
    public async Task<ActionResult> ChangePrivileges(){
      throw new NotImplementedException(); 
    }


    [HttpPost("ban")]
    public async Task<ActionResult> Ban() { 
      throw new NotImplementedException(); 
    }

    [HttpGet("ban")]
    public async Task<ActionResult> GetBan() {
      throw new NotImplementedException(); 
    }

    [HttpGet("unban")]
    public async Task<ActionResult> GetUnBan() {
      throw new NotImplementedException(); 
    }
}


