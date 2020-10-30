using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TennisClub.DAL.Repositories
{
    public class Repository<TEntity, TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO> : IRepository<TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO>
        where TEntity : class
        where TEntityCreateDTO : class
        where TEntityReadDTO : class
        where TEntityUpdateDTO : class
    {
        protected readonly TennisClubContext Context;
        protected readonly IMapper _mapper;

        public Repository(TennisClubContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public TEntityReadDTO Create(TEntityCreateDTO entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            TEntity mappedObject = _mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Add(mappedObject);
            Context.SaveChanges();

            return _mapper.Map<TEntityReadDTO>(mappedObject);
        }

        public virtual void Delete(TEntityReadDTO entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            TEntity mappedObject = _mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Remove(mappedObject);
        }

        public IEnumerable<TEntityReadDTO> Find(Expression<Func<TEntityReadDTO, bool>> predicate)
        {
            Expression<Func<TEntity, bool>> predicateToEntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            IQueryable<TEntity> itemsFromDB = Context.Set<TEntity>().Where(predicateToEntity);
            return _mapper.Map<IEnumerable<TEntityReadDTO>>(itemsFromDB);
        }

        public IEnumerable<TEntityReadDTO> GetAll()
        {
            List<TEntity> itemsFromDB = Context.Set<TEntity>().AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<TEntityReadDTO>>(itemsFromDB);

        }

        public TEntityReadDTO GetById(int id)
        {
            TEntity itemFromDB = Context.Set<TEntity>().Find(id);
            return _mapper.Map<TEntityReadDTO>(itemFromDB);
        }

        public TEntityCreateDTO GetCreateDTOByReadDTO(TEntityReadDTO entity)
        {
            return _mapper.Map<TEntityCreateDTO>(entity);
        }
        public TEntityUpdateDTO GetUpdateDTOByReadDTO(TEntityReadDTO entity)
        {
            return _mapper.Map<TEntityUpdateDTO>(entity);
        }

        public void MapUpdateDTOToReadDTO(TEntityUpdateDTO memberToPatch, TEntityReadDTO memberModelFromRepo)
        {
            _mapper.Map(memberToPatch, memberModelFromRepo);
        }

        public TEntityReadDTO SingleOrDefault(Expression<Func<TEntityReadDTO, bool>> predicate)
        {
            Expression<Func<TEntity, bool>> predicateToEntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            TEntity itemFromDB = Context.Set<TEntity>().SingleOrDefault(predicateToEntity);
            return _mapper.Map<TEntityReadDTO>(itemFromDB);
        }
    }
}
