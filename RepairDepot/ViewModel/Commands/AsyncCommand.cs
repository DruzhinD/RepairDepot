namespace RepairDepot.ViewModel.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _command;
        public AsyncCommand(Func<object, Task> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public async override Task ExecuteAsync(object parameter)
        {
            var task = _command(parameter);
            await task;
            
        }
    }
}
