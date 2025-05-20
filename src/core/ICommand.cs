


/*
 * Interface for commands through-out the project
 *
 * */
namespace DAL.command;
/*
 * Command interface for action / transaction in DB
 *
 * */
interface ICommand<TAction, TResponse> {
    Task<TResponse> Execute(TAction action); //does action
    void UnExecute(TAction action); //undoes action might be optional but HAS to be specified in doc if so
      //TODO: add to history list 
}
