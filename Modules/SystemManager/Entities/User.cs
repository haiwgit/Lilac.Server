using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.SystemManager.Entities
{
    [DataContract]
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [DataMember]
        public virtual string Account { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        public virtual User UnWrapNHibernateClass()
        {
            return this;
        }
    }
}
