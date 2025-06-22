using wiwi.infrastructure.repository.user;
using wiwi.infrastructure.models;

using wiwi.domain.factory;
using wiwi.domain.entities;
using wiwi.domain.service.auth.exception;

namespace wiwi.domain.service.auth;
/*
 * verify password in request
 *
 * */
public class AuthHandler: ILoginService {
  //public:
  private IUserRepository _repo;
  public ILoginService? next{get; set;}
    
  public AuthHandler(IUserRepository repo){
    _repo = repo;
  }

  /*
   * function to authentificate user on request 
   * if valid OR invalid it erases the password of the request
   *
   * */
  public void Handle(ref Request request){
    UserModel? model = _repo.FindByEmail(request.email);
    if(model is null)
      throw new CredentialsException();

    UserEntityFactory factory = new UserEntityFactory();
    User user = factory.CreateFromStorage(model);
 
    if (user.verifyCredentials(request.email, request.password)){
      request.user = user;
      next?.Handle(ref request);
    } else {
      throw new CredentialsException();
    }
  } 
}








