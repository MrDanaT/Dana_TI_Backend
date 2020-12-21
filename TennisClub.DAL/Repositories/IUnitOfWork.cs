using System;
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
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        IGameResultRepository GameResults { get; }
        IGenderRepository Genders { get; }
        ILeagueRepository Leagues { get; }
        IMemberFineRepository MemberFines { get; }
        IMemberRepository Members { get; }
        IMemberRoleRepository MemberRoles { get; }
        IRoleRepository Roles { get; }
        bool Commit();
    }
}