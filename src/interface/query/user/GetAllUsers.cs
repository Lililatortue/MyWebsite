using wiwi.interfaces.DTO;

using wiwi.infrastructure.repository.user;
using wiwi.infrastructure.models;

namespace wiwi.interfaces.query.user;
using MAP = wiwi.infrastructure.map.user;

public record Response(int errorCode, List<UserDTO>? list);


public class GetAllUsersQuery: IQuery<Response>{
  //variable
  private readonly IUserRepository _repo;
  private readonly (int min, int max) _range;

  //constructor

  public GetAllUsersQuery(IUserRepository repo,(int min, int max) range){
   _repo = repo; 
   _range = range;
  }


  //functions:
  
  public async Task<Response> Execute(){
    try{
    
      List<UserModel> model =  _repo.FetchAll(_range.min,_range.max);//transform list
      List<UserDTO>   user = MAP.UserModelMapping.ToDTO(model); 
      return new(200, user);
    } catch(Exception) {

      return new(500, null);
    }
  }

}
