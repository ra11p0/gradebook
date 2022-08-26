using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommands : BaseLogic<IFoundationCommandsRepository>, IFoundationCommands
{
    public FoundationCommands(IFoundationCommandsRepository repository) : base(repository)
    {
    }
}
