using System;

namespace EverestORM.Attributes
{
    /// <summary>
    /// Attribute for primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbPrimaryKeyAttr : Attribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DbPrimaryKeyAttr()
        {

        }
    }
}
