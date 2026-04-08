using AutoMapper;
using Lesson.Data;
using Microsoft.AspNetCore.DataProtection;

namespace Lesson.Services.Core
{
    public class BaseService<RepoType>: IBaseService<RepoType> where RepoType : class
    {

        protected readonly RepoType _repo;

        protected readonly IMapper _mapper;

        protected readonly AppDbContext _context;

        public BaseService(RepoType repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
    }
}
