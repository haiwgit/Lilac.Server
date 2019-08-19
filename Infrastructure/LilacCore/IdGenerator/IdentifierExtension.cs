using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.IdGenerator
{
    public static class IdentifierExtension
    {
        public static IdentityPart IfNullGenerateGuid(this IdentityGenerationStrategyBuilder<IdentityPart> col)
        {
            return col.Custom<NullGenerateGuidIDGenerator>();
        }
    }
}
