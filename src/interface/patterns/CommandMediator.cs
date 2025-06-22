using wiwi.interfaces.command;

using wiwi.infrastructure.diagnostic.logging;



namespace wiwi.interfaces.patterns.mediator;
/*
 * Modulorize the receiver 
 * */
public class CommandMediator<TAction> {
  private readonly Dictionary<Type, ICommand<TAction>> _commands;
  
  //private readonly ILogging _logs;

  public CommandMediator(/*ILogging logs*/){  
   // _logs = logs;
    _commands = new Dictionary<Type, ICommand<TAction>>();
  }

  /*  register command with action
   *  @param role ->of the user
   *  @param command -> install the record it uses is also the index of the command
   * */
  public void register<T>(ICommand<TAction> command) where T: TAction {
        _commands[typeof(T)] = command;
  }

  /*  sends a Task
   *  @param action -> record that contains the data
   * */
  public Task<(int errorCode, string? message)> send(TAction action){
    if(action is null) throw new ArgumentException("action is null");

    var taction = action.GetType();
    
    if(!_commands.TryGetValue(taction, out var entry)){
     // _logs.log("Command: "+action.GetType().Name + "failed method doesnt exist");
      throw new InvalidOperationException("no such thing as this methode");
    }

    return entry.Execute(action);     
  } 
}
