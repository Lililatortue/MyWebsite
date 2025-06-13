

/*
 *Abstract class for the chain of responsibility 
 *to allow more modular system to login.


 *To see more about the schema of the login go to:
 * */

namespace loginService;


public abstract class LoginHandler{

  //public:

  public void SetNext(LoginHandler handler){ this._next = handler; }
  public abstract void Handle(ref Request request);

  //protected:

  /*Goes to next handle if its not null else does nothing
   *
   * */
  protected void NextHandle(ref Request request){ _next?.Handle(ref request); }

  //private:

  private LoginHandler? _next;
}

