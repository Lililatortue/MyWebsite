using wiwi.infrastructure.models;
using wiwi.domain.entities;
using wiwi.interfaces.DTO;



namespace wiwi.infrastructure.map.user;


public static class UserModelMapping{
  
  public static UserModel ToModel(User user) {
    return new UserModel( 
                          user.Username,
                          user.Email,
                          user.Password,
                          user.Privilege.ToString().ToLower(),
                          user.BannedFlag);
  }
  
  public static UserDTO ToDTO(UserModel user){
    return new UserDTO(user.Username, user.Email, user.Role);

  }
  
  public static List<UserDTO> ToDTO(List<UserModel> models){
    List<UserDTO> listDTO = new List<UserDTO>(); 
    foreach(var m in models)
       listDTO.Add(ToDTO(m));

    return listDTO;
  }
}
