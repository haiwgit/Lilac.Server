using NHibernate;
using NHibernate.Context;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lilac.LilacCore.Utility
{
    public class DBOperator
    {
        public static readonly DBOperator Instance;

        static DBOperator()
        {
            Instance = new DBOperator(Nefarian.Core.WebServiceSite.GetAppContainer().Resolve<ISessionFactory>("Lilac"));
        }

        private ISessionFactory factory;

        private DBOperator(ISessionFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// 打开一个新的ISession
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession()
        {
#if DEBUG
            //return factory.OpenSession(NHSqlWatcher.Instance);
            return factory.OpenSession();
#else
			return factory.OpenSession();
#endif
        }

        /// <summary>
        /// 打开一个无状态的IStatelessSession
        /// </summary>
        /// <returns></returns>
        public IStatelessSession OpenStatelessSession()
        {
            return factory.OpenStatelessSession();
        }

        /// <summary>
        /// 获取与当前请求相关的ISession，同一个请求共享一个ISession对象。
        /// </summary>
        /// <returns></returns>
        public ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(factory))
            {
                CurrentSessionContext.Bind(this.OpenSession());
            }
            return factory.GetCurrentSession();
        }

        /// <summary>
        /// 释放当前请求关联的ISession
        /// </summary>
        internal void ReleaseCurrentSession()
        {
            if (CurrentSessionContext.HasBind(factory))
            {
                ISession session = CurrentSessionContext.Unbind(factory);
                if (session.IsOpen)
                    session.Close();
            }
        }
    }

#if DEBUG
    internal class NHSqlWatcher : EmptyInterceptor
    {
        public static readonly NHSqlWatcher Instance = new NHSqlWatcher();

        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.Print(sql.ToString());
            return base.OnPrepareStatement(sql);
        }
    }
#endif

}
