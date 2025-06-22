//interface
using wiwi.interfaces.DTO;

//infrastructure
using wiwi.infrastructure.models;

//domain
using wiwi.domain.entities;
using wiwi.domain.valueobjects.credentials;
using wiwi.domain.valueobjects.privilege;


namespace wiwi.domain.factory;

/* Simple factory to initialise users and handle logic of initialisation
 *
 * */
public class UserEntityFactory{

  /* Initialize a user meant to be used when u need to add a user meant for creating
   *
   * @param UserDTO(username, email, role)
   * @param password
   *
   * @return User
   * */
  public User CreateFromPasswordString(UserDTO dto, string password){
    var creds = Credentials.CreateCredentials(dto.GetEmail(), password); 
    
    return new User(dto.GetUsername(),creds,new (PrivilegeStatus.Guest), false);
  }
    
  /* Initialize a user that already exist in a db meant for validation 
   *
   * @param UserDTO(username, email, role)
   * @param password
   *
   * @return User
   * */
  public User CreateFromStorage(UserModel model){  
    var creds = Credentials.GetCredentials(model.Email,model.HashedPsw);
    Enum.TryParse(model.Role, out PrivilegeStatus status);

    return new User(model.Username, creds, new (status), model.Banned);
  }


}
