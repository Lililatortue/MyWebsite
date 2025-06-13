using wiwi.infrastructure.models;
using wiwi.domain.entities;
using interfaces.DTO;



namespace wiwi.infrastructure.map.user;


public static class UserModelMapping{
  
  public static UserModel ToModel(User user) {
    return new UserModel( 
                          user.Username,
                          user.Email,
                          user.Password,
                          user.Privilege.ToString(),
                          user.BannedFlag);
  }

  public static UserDTO ToDTO(UserModel user){
    return new UserDTO(user.Username, user.Email, user.Role);

  }

}
