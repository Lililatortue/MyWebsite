using wiwi.interfaces.command;
using wiwi.interfaces.DTO;

using wiwi.domain.entities;
using wiwi.domain.factory;

using wiwi.infrastructure.repository.user;

namespace user.command;
using MAP = wiwi.infrastructure.map.user;


public record TUnBanAction(UserDTO dto);

/*
 * Allows to manually unban user
 * */
public class UnBanUserCommand: ICommand<TUnBanAction>{
  //variables:
  private readonly IUserRepository _repo;
  private User? _state;
  //constructor:
  public UnBanUserCommand(IUserRepository repo){
    _repo = repo;
   }
 
  //function:
  
  public async Task<(int errorCode, string? message)> Execute(TUnBanAction action){
    try{
      var model = _repo.FindByEmail(action.dto.GetEmail()); 
      if(model == null) 
        return new (404, "user not found");
     
      UserEntityFactory factory = new UserEntityFactory();
      var user = factory.CreateFromStorage(model);
      _state = user; 

      user.SetBannedFlag(false);
      
      _repo.Update(MAP.UserModelMapping.ToModel(user));

      return new (200, null);
    } catch(Exception ex) {
      return new (500, ex.Message);
    }
  }

  public (int errorCode, string? message) UnExecute(){
    if(_state is null) throw new NullReferenceException("unBanUserCommand.UnExecute not yet finish");

    try{
      throw new NotImplementedException("not yet done");
      _repo.Update(MAP.UserModelMapping.ToModel(_state));

      return new (200, null);
    } catch(Exception) {
      
      return new (500, "unable to UnExecute");
    }
  }
}
