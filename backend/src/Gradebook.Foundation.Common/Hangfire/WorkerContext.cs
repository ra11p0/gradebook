namespace Gradebook.Foundation.Common.Hangfire;

public class WorkerContext
{
    private static Context? _context;
    public static Context? Context { get { return _context; } set { _context = value; } }
}
