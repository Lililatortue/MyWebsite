using context.user;
using map = mapping.user;

using security.logic;

namespace loginService;


/*
 * verify password in request
 *
 * */
public class AuthHandler: LoginHandler {
  //public:

  public AuthHandler(UserDbContext service){
    _service = service;
  }

  /*
   * function to authentificate user on request 
   * if valid OR invalid it erases the password of the request
   *
   * */
  public override void Handle(ref Request request){
    var user = _service.Users.Find(request.email); 

    using(Password psw = new Password(user.Password)){ // to use password as short as possible
      if(psw.CheckPassword(request.password)) {
        request.user= map.UserMapping.ModelToDTO(user);    
      } else {
        request.password = "";  //cleaning 
        throw new UnauthorizedAccessException();
      }
    }

    request.password =""; //cleaning
    this.NextHandle(ref request);
  } 

  //private:

  private readonly UserDbContext _service;
  

}
