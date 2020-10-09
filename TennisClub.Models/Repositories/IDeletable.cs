using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DAL.Repositories
{
    public interface IDeletable<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
    }
}
