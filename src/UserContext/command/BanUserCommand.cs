using DTO;
using DAL.command;
using context.user;
using map = user.mapping;

namespace user.command;

public record TBanUserResponse(int ErrorCode, string? message);
public record TBanUserAction(UserDTO dto);

/*
 * Ban user for now
 * future add user to table and log the ban
 *
 * */
public class BanUserCommand: ICommand<TBanUserAction, TBanUserResponse>{
  //variables

  public readonly UserDbContext _context;
  
  //constructor
  
  public BanUserCommand(UserDbContext context){
    _context = context;  
  }


  //functions:

  public async Task<TBanUserResponse> Execute(TBanUserAction action){
    try{
      //find user
      var user = await _context.Users.FindAsync(action.dto.GetEmail());
      //check user
      if(user == null)return new TBanUserResponse(404,"user not found");  
      
      user.SetBannedFlag(true); //ban him
      //TODO: add to ban table, reason why, statement said, timespan,
      

      _context.Users.Update(user);
      await _context.SaveChangesAsync();

      return new TBanUserResponse(200, null);
    }catch(Exception ex){
      return new TBanUserResponse(500, ex.Message);
    }
  }

  public void UnExecute(TBanUserAction action){
    //TODO: do UnExecute
  }
}
