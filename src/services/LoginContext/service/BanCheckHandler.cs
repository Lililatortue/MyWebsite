using UserModel;



namespace loginService;


/*
 * Checks if user is banned upon login
 *
 * */
public class BanCheckHandler: LoginHandler{
  
  public BanCheckHandler(BanService service){
    _service = service;
  }


  public override void Handle(ref Request request){
    
    if(!_service.isBanned(request.username)){
      this.NextHandle(ref request);
    } else {
      //TODO: return json
      throw new UnauthorizedAccessException(); 
    }
  }
  //private:
  

  private readonly BanService _service;
}
