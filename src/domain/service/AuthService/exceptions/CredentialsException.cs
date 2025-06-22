

namespace wiwi.domain.service.auth.exception;

public class CredentialsException: Exception{
  public CredentialsException()
     : base("Invalid Credentials") { }
}
