using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TennisClub.DAL.Repositories
{
    public abstract class Repository<TEntity, TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO> : IRepository<TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO>
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

        public virtual TEntityReadDTO Create(TEntityCreateDTO entity)
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

        public virtual void Delete(int id)
        {
            if (id < 0)
            {
                throw new NullReferenceException("Id is out of range");
            }

            TEntity itemFromDb = Context.Set<TEntity>().Find(id);

            if (itemFromDb != null)
            {
                Context.Set<TEntity>().Remove(itemFromDb);
            }
            else
            {
                throw new NullReferenceException("Object not found");
            }
        }

        public IEnumerable<TEntityReadDTO> Find(Expression<Func<TEntityReadDTO, bool>> predicate)
        {
            Expression<Func<TEntity, bool>> predicateToEntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            List<TEntity> itemsFromDB = Context.Set<TEntity>().Where(predicateToEntity).AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<TEntityReadDTO>>(itemsFromDB);
        }

        public virtual IEnumerable<TEntityReadDTO> GetAll()
        {
            List<TEntity> itemsFromDB = Context.Set<TEntity>().AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<TEntityReadDTO>>(itemsFromDB);
        }

        public virtual TEntityReadDTO GetById(int id)
        {
            if (id < 0)
            {
                throw new NullReferenceException("Id is out of range");
            }

            TEntity itemFromDB = Context.Set<TEntity>().Find(id);

            if (itemFromDB == null)
            {
                throw new NullReferenceException("Object not found");
            }

            return _mapper.Map<TEntityReadDTO>(itemFromDB);
        }

        public TEntityReadDTO SingleOrDefault(Expression<Func<TEntityReadDTO, bool>> predicate)
        {
            Expression<Func<TEntity, bool>> predicateToEntity = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            TEntity itemFromDB = Context.Set<TEntity>().SingleOrDefault(predicateToEntity);
            return _mapper.Map<TEntityReadDTO>(itemFromDB);
        }

        public virtual void Update(int id, TEntityUpdateDTO entity)
        {
            if (id < 0)
            {
                throw new NullReferenceException("Id is out of range");
            }

            TEntity itemFromDb = Context.Set<TEntity>().Find(id);

            if (itemFromDb != null)
            {
                _mapper.Map(entity, itemFromDb);
            }
            else
            {
                throw new NullReferenceException("Object not found");
            }
        }
    }
}
