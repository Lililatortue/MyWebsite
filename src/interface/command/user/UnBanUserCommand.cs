using context.user;
using DAL.command;

using DTO;
using models.user;

namespace user.command;

public record TUnBanAction(UserDTO dto);

/*
 * Allows to manually unban user
 * */
public class UnBanUserCommand: ICommand<TUnBanAction>{
  //variables:
  private readonly UserDbContext _context;
  private readonly TUnBanAction _action;
  private UserModel? _state;
  //constructor:
  public UnBanUserCommand(UserDbContext context, UserDTO dto){
    _context = context;
    _action  = new TUnBanAction(dto); 
  }

  
  //function:
  
  public async Task<Response> Execute(){
    try{
      var user = await _context.Users.FindAsync(_action.dto.GetEmail()); 
      if(user == null) 
        return new Response(404, "user not found");
     
      _state = user;
      
      user.SetBannedFlag(false);
 
      _context.Users.Update(user);
      await _context.SaveChangesAsync();

      return new Response(200, null);
    } catch(Exception ex) {
      return new Response(500, ex.Message);
    }
  }

  public Response UnExecute(){
    if(_state is null) throw new NullReferenceException("none existante state");

    try{
      _context.Users.Update(_state);
      _context.SaveChanges();

      return new Response(200, null);
    } catch(Exception) {
      
      return new Response(500, "unable to UnExecute");
    }
  }
}
