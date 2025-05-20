using DTO;
using models;
/*
 * Adapter concreteClass that allows easy transition between UserModel and UserDTO
 *
 * */
namespace user.mapping;


public class UserMapping {
  
  public static UserDTO SourceToTarget(UserModel model){ 
    return new UserDTO(model.username, model.email, model.role);
  }

  public static UserModel TargetToSource(UserDTO DTO, string password){
    return new UserModel(DTO.GetUsername(), DTO.GetEmail(), DTO.GetRole(), password);
  } 

}
