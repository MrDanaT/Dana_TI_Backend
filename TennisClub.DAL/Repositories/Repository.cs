using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common;

namespace TennisClub.DAL.Repositories
{
    public abstract class Repository<TEntity, TEntityCreateDTO, TEntityReadDTO, TEntityUpdateDTO> : IRepository<
        TEntityCreateDTO,
        TEntityReadDTO, TEntityUpdateDTO>
        where TEntity : class
        where TEntityCreateDTO : class
        where TEntityReadDTO : class
        where TEntityUpdateDTO : class
    {
        protected readonly IMapper _mapper;
        protected readonly TennisClubContext Context;

        protected Repository(TennisClubContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public virtual TEntityReadDTO Create(TEntityCreateDTO entity)
        {
            if (entity.IsNull()) throw new ArgumentNullException(nameof(entity));

            var mappedObject = _mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Add(mappedObject);
            Context.SaveChanges();

            return _mapper.Map<TEntityReadDTO>(mappedObject);
        }

        public virtual void Delete(int id)
        {
            if (!id.IsValidId()) throw new NullReferenceException("Id is out of range");

            var itemFromDb = Context.Set<TEntity>().Find(id);

            if (!itemFromDb.IsNull())
                Context.Set<TEntity>().Remove(itemFromDb);
            else
                throw new NullReferenceException("Object not found");
        }

        public virtual IEnumerable<TEntityReadDTO> GetAll()
        {
            var itemsFromDB = Context.Set<TEntity>().AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<TEntityReadDTO>>(itemsFromDB);
        }

        public virtual TEntityReadDTO GetById(int id)
        {
            if (!id.IsValidId()) throw new NullReferenceException("Id is out of range");

            var itemFromDB = Context.Set<TEntity>().Find(id);

            if (itemFromDB.IsNull()) throw new NullReferenceException("Object not found");

            return _mapper.Map<TEntityReadDTO>(itemFromDB);
        }

        public virtual void Update(int id, TEntityUpdateDTO entity)
        {
            if (!id.IsValidId()) throw new NullReferenceException("Id is out of range");

            var itemFromDb = Context.Set<TEntity>().Find(id);

            if (!itemFromDb.IsNull())
                _mapper.Map(entity, itemFromDb);
            else
                throw new NullReferenceException("Object not found");
        }
    }
}