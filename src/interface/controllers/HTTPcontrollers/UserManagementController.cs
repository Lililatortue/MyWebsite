using Microsoft.AspNetCore.Mvc;

using interfaces.DTO;




namespace interfaces.controller;


[ApiController]
[Route("api/user/")]
public class UserManagmentController{

    //PATCH: api/user/update()
    [HttpPatch("/privileges")]
    public async Task<ActionResult> ([FromBody]PrivilegesRequest request ){
      request.email, request.PRIVILEGES;
    }

    //POST: api/user/ban
    [HttpPost("ban")]
    public async Task<ActionResult> Ban(){
      
    }

    //POST: api/user/unban
    [HttpPost("unban")]
    public async Task<ActionResult> UnBan(){
      
    }
}


public record PrivilegesRequest(String email, PRIVILEGES){};
