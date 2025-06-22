using wiwi.interfaces.DTO;
using wiwi.interfaces.command;

using wiwi.infrastructure.repository.user;
using wiwi.infrastructure.models;

using wiwi.domain.entities;
using wiwi.domain.factory;

namespace interfaces.command.user;
using MAP = wiwi.infrastructure.map.user;


public record TBanUserAction(UserDTO dto);

/*
 * Ban user for now
 * future add user to table and log the ban
 *
 * */
public class BanUserCommand: ICommand<TBanUserAction>{
  //variables

  private readonly IUserRepository _repo;
  private User? _state; //capture state for undo might check for memento pattern
  
  //constructor
  
  public BanUserCommand(IUserRepository repo){
    _repo = repo; 

  }


  //functions:

  public async Task<(int errorCode, string?message)> Execute(TBanUserAction action){
    try{
      //find user
      UserModel? model = _repo.FindByEmail(action.dto.email);
      if(model is null)
        return (404,"user not found"); 
      
      UserEntityFactory factory = new UserEntityFactory();
      var user = factory.CreateFromStorage(model); 
      //make sure action has the same data has user for potentiel UnExecute operation
      _state = user;
      
      user.SetBannedFlag(true); //ban him
      //TODO:(Ban table)-> reason why, statement said, timespan,
      _repo.Update(MAP.UserModelMapping.ToModel(user));
      
      return (200, null);
    } catch(Exception) {
      return (500,"Execute fail");
    }
  }
  

  public (int errorCode, string?message) UnExecute(){
    if(_state is null) throw new NullReferenceException("State is not captured");

    try{
      _repo.Update(MAP.UserModelMapping.ToModel(_state));
      return (200, "BanUser commande undone");

    } catch(Exception) {
      return (500,"UnExecute fail");
    }
  }



}







