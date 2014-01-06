using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDoge.Entities
{
    // Defines an interface for entities having an Id as primary key (useful for testing of the repositories)
    public interface IEntity
    {
        int Id { get; }
    }
}
