


/*
 * Interface for commands through-out the project
 *
 * */
namespace interfaces.command;
/*
 * Command interface for action / transaction in DB
 *
 * */

public interface ICommand<TAction> {
    public Task<(int errorCode, string? message)> Execute(TAction action); //does action
    public (int errorCode, string? message) UnExecute(); //undoes action might be optional but HAS to be specified in doc if so
      //TODO: add to history list 
}
