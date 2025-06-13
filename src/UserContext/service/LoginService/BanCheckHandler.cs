using context.user;


namespace loginService;


/*
 *  Checks if user is banned upon login
 *  for now no distinct tables for check just a flag so no need for service
 * */
public class BanCheckHandler: LoginHandler{
  
  public BanCheckHandler(UserDbContext service){
    _service = service;
  }


  public override void Handle(ref Request request){
    
    if(request.user.isBanned()){
      this.NextHandle(ref request);
    } else {
      //TODO: return json
      throw new UnauthorizedAccessException(); 
    }
  }
  //private:
  

  private readonly UserDbContext _service;
}
