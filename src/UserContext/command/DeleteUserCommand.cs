using DTO;
using DAL.command;
using context.user;



namespace user.command;

public record TDeleteUserResponse(int errorCode, string? message);
public record TDeleteUserAction(UserDTO dto);

/*
 * Deletes a user in the db.
 * DOES SUPPORT UnExecute
 * */

public class DeleteUserCommand: ICommand<TDeleteUserAction, TDeleteUserResponse>{
  //variables:
  private readonly UserDbContext _context;

  //constructor:
  
  public DeleteUserCommand(UserDbContext context){
    _context = context;
  }

  
  public async Task<TDeleteUserResponse> Execute(TDeleteUserAction action){
    try{
      //finds user
      var user = await _context.Users.FindAsync(action.dto.GetEmail()); 

      //check if user exist
      if(user == null) return new TDeleteUserResponse(404, "user not found");


      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return new TDeleteUserResponse(200, null);
    } catch(Exception ex) {

      return new TDeleteUserResponse(500, ex.Message);
    }
  }
  
  public void UnExecute(TDeleteUserAction action){
    //TODO: implement UnExecute
  }
}
