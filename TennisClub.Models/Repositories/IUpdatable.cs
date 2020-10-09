namespace TennisClub.DAL.Repositories
{
    public interface IUpdatable<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        bool SaveChanges();
    }
}