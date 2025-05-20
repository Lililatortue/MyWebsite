using Microsoft.AspNetCore.Mvc;

using DTO;
using user.command;



namespace user.controller;

  [ApiController]
  [Route("api/user")]
  public class UserController{
    
    private readonly Mediator _mediator;
    // TODO: add -> private readonly HistoryList list;

    //POST: api/user/create
    [HttpPost("create")]
    public async Task<ActionResult> Create(){
      

    }
    
    //DELETE: api/user/delete
    [HttpDelete("delete")]
    public async Task<ActionResult> Delete(){
      

    }

    //PATCH: api/user/update()
    [HttpPatch("update")]
    public async Task<ActionResult> Update(){
      

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
