



namespace DAL.query;

public interface IQuery<TResponse>{
  Task<TResponse> Execute();
}

