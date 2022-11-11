using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Students;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    //private readonly ServiceResolver<IMapper> _mapper;
    //private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public StudentsController(IServiceProvider serviceProvider)
    {
        //_mapper = serviceProvider.GetResolver<IMapper>();
        //_foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
}
