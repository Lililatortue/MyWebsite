



namespace wiwi.interfaces.query;

public interface IQuery<TResponse>{
  Task<TResponse> Execute();
}

