using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business
{
    public class AuthorService : BaseService<Author, CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>, IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepo authorRepo, IMapper mapper) : base(authorRepo, mapper)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        public async Task<GetAuthorDto> CreateOne(CreateAuthorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (await _authorRepo.GetOneByAuthorName(dto.AuthorName) != null) throw new Exception($"Author with name {dto.AuthorName} already exists");

            var newAuthor = _mapper.Map<Author>(dto);
            return _mapper.Map<GetAuthorDto>(await _authorRepo.CreateOne(newAuthor));
        }

    }
}