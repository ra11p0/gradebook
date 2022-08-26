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

    public async Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
    {
        var student = _mapper.Map<Student>(newStudentDto);

        var res = await Context.Students.AddAsync(student);

        return new ResponseWithStatus<bool>(true);
    }
}
