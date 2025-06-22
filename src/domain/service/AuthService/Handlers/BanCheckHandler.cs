using wiwi.domain.service.auth.exception;

namespace wiwi.domain.service.auth;

/*
 *  Checks if user is banned upon login
 *  for now no distinct tables for check just a flag so no need for service
 * */
public class BanCheckHandler: ILoginService{
  public ILoginService? next{get;set;}  

  public void Handle(ref Request request){
    if (request.user is null)
      throw new NullReferenceException("user not found");
    
    if(!request.user.BannedFlag){
      next?.Handle(ref request);
    } else {
      throw new BanException(request.email); 
    }
  }

}
