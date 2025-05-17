using UserModel;
using DbConnection;

namespace loginService;

public class AuthHandler: LoginHandler {
  //public:

  public override void Handle(ref Request request){
    
    //TODO: create appropriate service to fetch users;
    DbConnection _conn = new DbConnection();

    (UserDTO user,string rawPsw) = _conn.GetUser(request.username); 
    //TODO: make it return a char[] to force it on the stack 
    using Password psw = new Password(rawPsw);
    rawPsw = "";


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



}
