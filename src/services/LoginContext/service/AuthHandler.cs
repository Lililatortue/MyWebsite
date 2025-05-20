using UserModel;

namespace loginService;

public class AuthHandler: LoginHandler {
  //public:
  public AuthHandler(IDbService<UserDTO, string> service){
    _service = service;
  }


  public override void Handle(ref Request request){

    (UserDTO user, Span<char*> rawPsw = stackalloc char[49]) = _service.FindOne(request.username); 
     
    using Password psw = new Password(rawPsw);

    if(psw.CheckPassword(request.password)) {
      request.user=user;  
      request.password = "";// cleaning
      this.NextHandle(ref request);

    } else {
      request.user = user;
      request.password = "";  //cleaning 
      throw new UnauthorizedAccessException();
    }
    
  } 

  //private:

  private readonly IDbService _service;

}
