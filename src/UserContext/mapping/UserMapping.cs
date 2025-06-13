using DTO;
using models.user;
/*
 * Adapter concreteClass that allows easy transition between UserModel and UserDTO
 *
 * */
namespace mapping.user;


public class UserMapping {
  
  public static UserDTO ModelToDTO(UserModel model){ 
    return new UserDTO(model.Username, model.Email, model.Role);
  }
 

  public static IEnumerable<UserDTO> ModelToDTO(IEnumerable<UserModel> models){
    foreach(var model in models) {

        yield return ModelToDTO(model);
        
    }
  }


  public static UserModel DTOToModel(UserDTO DTO, string password){
    return new UserModel(DTO.GetUsername(), DTO.GetEmail(), DTO.GetRole(), password);
  } 

}
