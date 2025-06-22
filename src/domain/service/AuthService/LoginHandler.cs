

/*
 *Abstract class for the chain of responsibility 
 *to allow more modular system to login.


 *To see more about the schema of the login go to:
 * */

namespace wiwi.domain.service.auth;


public interface ILoginService{

  //private:

  public ILoginService? next{get;set;}

  //public:
  public abstract void Handle(ref Request request);

}

