



namespace wiwi.infrastructure.diagnostic.logging;

public interface ILogging {
  public void log(String message);  
}

public enum LogLevel {
    INFO  = 0,
    WARN  = 1,
    ERROR = 2,
    FAIL  = 3,
}
