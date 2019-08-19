using NHibernate;
using NHibernate.Id;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.IdGenerator
{
    public class NullGenerateGuidIDGenerator : IIdentifierGenerator, IConfigurable
    {
        string entityName;

        public object Generate(NHibernate.Engine.ISessionImplementor session, object obj)
        {

            object id = session.GetEntityPersister(entityName, obj).GetIdentifier(obj, session.EntityMode);
            if (id == null)
            {
                id = Guid.NewGuid().ToString();
            }
            return id;
        }

        public void Configure(IType type, IDictionary<string, string> parms, NHibernate.Dialect.Dialect dialect)
        {
            parms.TryGetValue(IdGeneratorParmsNames.EntityName, out entityName);
            if (entityName == null)
            {
                throw new MappingException("no entity name");
            }
        }
    }
}
