using HotDoge.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotDoge.Tests.Persistence
{
    /// <summary>
    /// Fake Db Set used to unit test the repository. Some methods like Find or Attach try to mimic the Entity Framework behavior to allow for testing of the repository. 
    /// Not entirely sure this is the right approach, though
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeDbSet<T> : IDbSet<T>
       where T : class, new()
    {
        private readonly ObservableCollection<T> _collection;

        public FakeDbSet()
            : this(new T[0])
        {
        }

        /// <summary>
        /// Constructor with some initial values
        /// </summary>
        public FakeDbSet(params T[] initialValues)
        {
            _collection = new ObservableCollection<T>();
            foreach (var initial in initialValues)
            {
                if (initial != null)
                    _collection.Add(initial);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public Expression Expression
        {
            get { return _collection.AsQueryable().Expression; }
        }

        public Type ElementType
        {
            get { return _collection.AsQueryable().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _collection.AsQueryable().Provider; }
        }

        public T Find(params object[] keyValues)
        {
            if (!(typeof(IEntity).IsAssignableFrom(typeof(T)))) // checks whether generic type T implements interface IEntity (and thus contain a property [Id] we'll be able to search upon)
            { 
                throw new ArgumentException(string.Format("Entity [{0}] does not contain a property [Id], so it could not be converted to the IEntity interface, used in this function.", typeof(T).ToString())); 
            }
            return this.SingleOrDefault(e => (e as IEntity).Id == (int)keyValues.Single());
        }

        public T Add(T entity)
        {
            _collection.Add(entity);

            return entity;
        }

        public T Remove(T entity)
        {
            _collection.Remove(entity);

            return entity;
        }

        public T Attach(T entity)
        {
            if (!_collection.Contains(entity) && entity != null)
            {
                return Add(entity);
            }
            else { return entity; }
        }

        public T Create()
        {
            return new T();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { return _collection; }
        }
    }
}