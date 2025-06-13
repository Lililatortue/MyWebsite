using context.user;
using DAL.query;

using DTO;
using models.user;

using map = mapping.user;

namespace query.user;
public record Response(int errorCode, List<UserDTO>? list);


public class GetAllUsersQuery: IQuery<Response>{
  //variable
  private readonly UserDbContext _context;
  private readonly (int min, int max) _range;

  //constructor

  public GetAllUsersQuery(UserDbContext context,(int min, int max) range){
   _context = context; 
   _range = range;
  }


  //functions:
  
  public async Task<Response> Execute(){
    try{
    
      List<UserModel> users = await _context.Users
                              .Where(u => u.id >=_range.min && u.id <=_range.max)
                              .ToListAsync();


      List<UserDTO> list = map.UserMapping.ModelToDTO(users).ToList(); //transform list
      
      return new(200, list);
    } catch(Exception) {

      return new(500, null);
    }
  }

}
