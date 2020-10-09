namespace TennisClub.DAL.Repositories
{
    public interface IDeletable<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
    }
}
