using Commands


/*
 * Modulorize the receiver 
 * */
public class Mediator<TAction, TResponse> {
  private readonly Dictionary<Type, object> commands;

  public Mediator(){
    commands = { LoginCommande, }
  }

  public TResponse send(TAction action){
    return 
  } 
}
