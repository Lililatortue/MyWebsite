using DAL.command;
using context.user; 
using DTO;
using map = user.mapping;



namespace user.command;

public record TAddUserResponse(int ErrorCode, string? message);
public record TAddUserAction(UserDTO user, string password);

/*
 *  Commande Responsible for adding a user to a db
 *  If succes then sends => 200 && < 300 
 *  else fail + message
 *
 *  DOES SUPPORT UnExecute function
 * */
public class AddUserCommand: ICommand<TAddUserAction, TAddUserResponse> {
  //variable:

  private readonly UserDbContext _context;
 
  //constructor:

  public AddUserCommand(UserDbContext context){
    _context = context;
  }


  /*
   *  Add User to Db;
   *  Register's it to an history list
   * */
  public async Task<TAddUserResponse> Execute(TAddUserAction action){
    
    string validPsw = logic.Password.CreatePassword(action.password);//salting + hashing

    try {

      var user = map.UserMapping.TargetToSource(action.user, action.password);
      _context.Users.Add(user); 
      await _context.SaveChangesAsync();

      return new TAddUserResponse(200, null);
    } catch(Exception ex) {//TODO: find appropriate Exception class

      return new TAddUserResponse(500, ex.Message);
    } 
  }


  //TODO: implement UnExecute
  public void UnExecute(TAddUserAction action){

  }
}
