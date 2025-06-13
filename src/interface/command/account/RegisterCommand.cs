
using wiwi.infrastructure.repository;

using interfaces.DTO;
using wiwi.domain.factory;
using wiwi.domain.entities;



namespace interfaces.command.account;

using map = wiwi.infrastructure.map.user;

public record TRegisterAction (UserDTO user, string password);

/*
 *  Commande Responsible for adding a user to a db
 *  If succes then sends => 200 && < 300 
 *  else fail + message
 *
 *  DOES SUPPORT UnExecute function
 * */
public class RegisterCommand: ICommand<TRegisterAction> {
  //variable:

  private readonly UserRepository _repo;
  private User? _state; 
  //constructor:

  public RegisterCommand(UserRepository repository){
    _repo = repository;
  }


  /*
   *  Add User to Db;
   *  Register's it to an history list
   * */
  public async Task<(int errorCode, string?message)> Execute(TRegisterAction action){
    try {
      UserEntityFactory factory = new UserEntityFactory();
      var user = factory.CreateFromPasswordString(action.user,action.password);
      _state = user; 
      _repo.Create(map.UserModelMapping.ToModel(user));


      return (200, null);
    } catch(Exception ex) {//TODO: (Exception)-> create or find appropriate Exception class

      return (500, ex.Message);
    } 
  }


  /*
   * Undo the execute commande 
   * Is not async.
   *  TODO: (ICommand.UnExecute)-> check for implementation of history list function should not be public
   * */
  public (int errorCode, string?message) UnExecute(){
    if(_state is null) throw new ArgumentException("state is null");
    try { 
      var user = _repo.FindByEmail(_state.Email);
      
      if(user is null) return (404,"user not found");
      
      _repo.Delete(user);


      return (200, null);
    } catch(Exception ex) {//TODO: (Exception)-> create or find appropriate Exception class:
      
      return (500, ex.Message);
    }
  }
}
