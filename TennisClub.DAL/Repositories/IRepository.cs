using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TennisClub.DAL.Repositories
{
    public interface IRepository<TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO>
        where TEntityCreateDTO : class
        where TEntityReadDTO : class
        where TEntityUpdateDTO : class
    {
        TEntityReadDTO Create(TEntityCreateDTO entity);
        void Delete(TEntityReadDTO entity);
        IEnumerable<TEntityReadDTO> GetAll();
        TEntityReadDTO GetById(int id);
        IEnumerable<TEntityReadDTO> Find(Expression<Func<TEntityReadDTO, bool>> predicate);
        TEntityReadDTO SingleOrDefault(Expression<Func<TEntityReadDTO, bool>> predicate);
        TEntityCreateDTO GetCreateDTOByReadDTO(TEntityReadDTO entity);
        TEntityUpdateDTO GetUpdateDTOByReadDTO(TEntityReadDTO entity);
        void MapUpdateDTOToReadDTO(TEntityUpdateDTO memberToPatch, TEntityReadDTO memberModelFromRepo);
    }
}
