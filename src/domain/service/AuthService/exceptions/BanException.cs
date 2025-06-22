



namespace wiwi.domain.service.auth.exception;

public class BanException: Exception {
  public BanException(String username)
     : base($"{username} fuck off"){ }
}
