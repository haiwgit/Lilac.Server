using FluentNHibernate.Mapping;
using Lilac.SystemManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.SystemManager.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap()
        {
            Not.LazyLoad();
            Table("sys_users");
            Id(p => p.ID, "ID");
            Map(p => p.Account, "ACCOUNT");
            Map(p => p.Name, "NAME");
            Map(p => p.Password, "PASSWORD");
        }
    }
}
