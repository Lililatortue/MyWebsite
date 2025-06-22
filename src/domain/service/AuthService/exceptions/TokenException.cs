

namespace wiwi.domain.service.auth.exception;

public class TokenException: Exception{
  public TokenException():base("Token creation failed") { }
}
