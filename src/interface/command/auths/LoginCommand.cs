using Microsoft.AspNetCore.Mvc;

using wiwi.domain.service.auth;




namespace wiwi.interfaces.command.auth;

public record TLoginAction(String email, String password){}

public class LoginCommand: ICommand<TLoginAction>{
  public readonly ILoginService _service;
  
  public LoginCommand(ILoginService service){
    _service = service;
  }

  public async Task<(int errorCode, string? message)> Execute(TLoginAction action){
    try {
      Request request = new Request();
      request.email = action.email;
      request.password = action.password;

      _service.Handle(ref request); 

      return (200, request.Token);
    } catch(Exception ex){
      return (500, ex.Message);
    }
    
  }
  
  public (int errorCode, string? message) UnExecute(){
    return(200, "Login doesnt support unexecute");  
  }

}
