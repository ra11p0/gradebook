using Gradebook.Foundation.Common;
using Gradebook.Foundation.Database;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepository : BaseRepository<FoundationDatabaseContext>, IFoundationCommandsRepository
{
    public FoundationCommandsRepository(FoundationDatabaseContext context) : base(context)
    {
    }
}
