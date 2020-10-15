using AutoMapper;
using TennisClub.DAL.Repositories.GameRepositoryFolder;
using TennisClub.DAL.Repositories.GameResultRepositoryFolder;
using TennisClub.DAL.Repositories.GenderRepositoryFolder;
using TennisClub.DAL.Repositories.LeagueRepositoryFolder;
using TennisClub.DAL.Repositories.MemberFineRepositoryFolder;
using TennisClub.DAL.Repositories.MemberRepositoryFolder;
using TennisClub.DAL.Repositories.MemberRoleRepositoryFolder;
using TennisClub.DAL.Repositories.RoleRepositoryFolder;

namespace TennisClub.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TennisClubContext _context;
        private readonly IMapper _mapper;
        private GameRepository _gameRepository;
        private GameResultRepository _gameResultRepository;
        private GenderRepository _genderRepository;
        private LeagueRepository _leagueRepository;
        private MemberFineRepository _memberFineRepository;
        private MemberRepository _memberRepository;
        private MemberRoleRepository _memberRoleRepository;
        private RoleRepository _roleRepository;

        public UnitOfWork(TennisClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IGameRepository Games => _gameRepository ??= new GameRepository(_context, _mapper);

        public IGameResultRepository GameResults => _gameResultRepository ??= new GameResultRepository(_context, _mapper);

        public IGenderRepository Genders => _genderRepository ??= new GenderRepository(_context, _mapper);

        public ILeagueRepository Leagues => _leagueRepository ??= new LeagueRepository(_context, _mapper);

        public IMemberFineRepository MemberFines => _memberFineRepository ??= new MemberFineRepository(_context, _mapper);

        public IMemberRepository Members => _memberRepository ??= new MemberRepository(_context, _mapper);

        public IMemberRoleRepository MemberRoles => _memberRoleRepository ??= new MemberRoleRepository(_context, _mapper);

        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context, _mapper);

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
