using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class CommandQueue
{
    private readonly Queue<ICommand> _commandsToExecute = new();
    private bool _runningCommand = false;

    public void AddCommand(ICommand commandToEnqueue)
    {
        _commandsToExecute.Enqueue(commandToEnqueue);
        RunNextCommand().Forget();
    }

    private async UniTask RunNextCommand()
    {
        if (_runningCommand)
        {
            return;
        }

        while (_commandsToExecute.Count > 0)
        {
            _runningCommand = true;
            var commandToExecute = _commandsToExecute.Dequeue();
            await commandToExecute.Execute();
        }

        _runningCommand = false;
    }
}