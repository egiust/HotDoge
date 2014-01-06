using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDoge.Persistence
{
    /// <summary>
    /// Unit Of Work is responsible to call the Save() method on the context it holds a reference to. 
    /// The same context is shared between all repositories contributing to the action ( a call to a business service method, for instance)
    /// For this application, the unicity of the context is enforced by Ioc (see HotDoge.Ioc.UnityConfig)
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }


    // Such Unit of Work. So Pattern. Wow.
    public class DogeUnitOfWork : IUnitOfWork
    {
        private readonly IDogeContext _dbContext;

        public DogeUnitOfWork(IDogeContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.Save();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
