using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issuing.Persistence
{
    public class RepositoryBase<T> : ICrudRepository<T> where T : class
    {
        private readonly ISession session;

        //public RepositoryBase()
        //{
        //}

        public RepositoryBase(ISession session)
        {
            this.session = session;
        }

        public void Delete(T Entitiy)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
           // using (ISession session = NHibernateSqLiteContext.SessionOpen())
           // {
                return session.Query<T>().ToList();

           // }
        }

        public T GetById(int id)
        {
           // using (ISession session = NHibernateSqLiteContext.SessionOpen())
           // {
                return session.Get<T>(id);

          //  }
        }

        public void Insert(T entity)
        {
           // using (ISession session = NHibernateSqLiteContext.SessionOpen())
           // {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(entity);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                            transaction.Rollback();
                        throw;
                    }
                }
                   
           // }
        }

        public void Update(T Entitiy)
        {
            throw new NotImplementedException();
        }
    }
}
