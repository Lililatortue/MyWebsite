using DTO;
using DAL.command;
using context.user;


namespace user.command;

public record TUnBanResponse(int ErrorCode, string? message);
public record TUnBanAction(UserDTO dto);

/*
 * Allows to manually unban user
 * */
public class UnBanUserCommand: ICommand<TUnBanAction, TUnBanResponse>{
  //variables:
  private readonly UserDbContext _context;
  
  //constructor:
  public UnBanUserCommand(UserDbContext context){
    _context = context;
  }

  
  //function:
  
  public async Task<TUnBanResponse> Execute(TUnBanAction action){
    try{
      var user = await _context.Users.FindAsync(action.dto.GetEmail());
      
      if(user == null) return new TUnBanResponse(404, "user not found");
      user.SetBannedFlag(false);
 
      _context.Users.Update(user);
      await _context.SaveChangesAsync();

      return new TUnBanResponse(200, null);
    } catch(Exception ex) {
      return new TUnBanResponse(500, ex.Message);
    }
  }

  public void UnExecute(TUnBanAction action){
    throw new NotImplementedException();
  }
}
