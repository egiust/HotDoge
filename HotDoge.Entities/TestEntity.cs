using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotDoge.Entities
{
    public class TestEntity : IEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string YetOtherProp { get; set; }
        public string Blah { get; set; }
    }
}