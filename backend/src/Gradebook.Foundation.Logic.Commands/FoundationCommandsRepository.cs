using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Domain.Models;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepository : BaseRepository<FoundationDatabaseContext>, IFoundationCommandsRepository
{
    private readonly IMapper _mapper;
    public FoundationCommandsRepository(FoundationDatabaseContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<ResponseWithStatus<bool>> ActivateAdministrator(ActivateAdministratorCommand command)
    {
        var school = _mapper.Map<School>(command.School);
        var administrator = _mapper.Map<Administrator>(command.Administrator);
        
        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
    {
        var student = _mapper.Map<Student>(newStudentDto);

        await Context.Students.AddAsync(student);

        return new ResponseWithStatus<bool>(true);
    }
}
