



namespace DAL.query;

public interface IQuery<TResponse>{
  TResponse Execute();
}

