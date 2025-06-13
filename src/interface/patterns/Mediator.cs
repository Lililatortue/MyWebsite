using interfaces.command;
using wiwi.domain.valueobjects.privilege;

using wiwi.infrastructure.diagnostic.logging;
/*
 * Modulorize the receiver 
 * */
public class CommandMediator<TAction> {
  private readonly Dictionary<Type, (PrivilegeStatus role, ICommand<TAction>command)> _commands;
  
  private readonly PrivilegeStatus _role;
  private readonly ILogging _logs;

  public CommandMediator(PrivilegeStatus role, ILogging logs){  
    _role = role;// role of user 
    _logs = logs;
    _commands = new Dictionary<Type,(PrivilegeStatus, ICommand<TAction>)>();
  }

  /*  register command with action
   *  @param role ->of the user
   *  @param command -> install the record it uses is also the index of the command
   * */
  public void register<T>(PrivilegeStatus role, ICommand<TAction> commands) where T: TAction {
        _commands[typeof(T)] = (role, commands);
  }

  /*  sends a Task
   *  @param action -> record that contains the data
   * */
  public Task<(int error, string? message)> send(TAction action){
    if(action is null) throw new ArgumentException("action is null");

    var taction = action.GetType();
    
    if(!_commands.TryGetValue(taction, out var entry)){
      _logs.log("Command: "+action.GetType().Name + "failed method doesnt exist");
      throw new InvalidOperationException("no such thing as this methode");
    }

    if(_role < entry.role){
      _logs.log("Command: "+entry.command.GetType().Name + "lack of rights needed");
      throw new InvalidOperationException("invalid rights");
    }

    return entry.command.Execute(action);     
  } 
}
