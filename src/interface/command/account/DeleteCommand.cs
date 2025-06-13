using interfaces.command;
using wiwi.domain.entities;
using wiwi.domain.factory;
using wiwi.infrastructure.repository;



namespace interfaces.command.account;

using map = wiwi.infrastructure.map.user;
public record TDeleteAccountAction(String email);
/*
 * Deletes a user in the db.
 * DOES SUPPORT UnExecute
 * */
public class DeleteCommand: ICommand<TDeleteAccountAction>{
  //variables:
  private readonly UserRepository _repo;
  private User? _state;

  //constructor:
  
  public DeleteCommand(UserRepository repo){
    _repo = repo;
  }

  
  public async Task<(int errorCode, string?message)> Execute(TDeleteAccountAction action){
    try{
      //finds user
      var user = _repo.FindByEmail(action.email); 
      //check if user exist
      if(user is null) 
        return (404, "user not found");

      UserEntityFactory factory = new UserEntityFactory();
      _state = factory.CreateFromStorage(user);

      _repo.Delete(user);
      await _repo.SaveChangesAsync();

      return (200, null);
    } catch(Exception ex) {

      return (500, ex.Message);
    }
  }
  


  public (int errorCode, string?message) UnExecute(){
    if(_state is null) throw new NullReferenceException("state is null");
    
    try {
      _repo.Create(map.UserModelMapping.ToModel(_state));
      _repo.SaveChanges();

      return (200, null);
    } catch(Exception ex) {
      return (500, ex.Message);
    }
  }

}
