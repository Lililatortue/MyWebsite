using wiwi.interfaces.command;




namespace interfaces.command.user;

public record TUpdateAction(String email, String username){};

public class UpdateUserCommand: ICommand<TUpdateAction>{

  public Task<(int errorCode, string? message)> Execute(TUpdateAction action){
    throw new NotImplementedException("UpdateUserCommand.Execute not yet implemented");
  }

  public (int errorCode, string? message) UnExecute(){
    throw new NotImplementedException("UpdateUserCommand.UnExecute not yet implemented");
  }
}
