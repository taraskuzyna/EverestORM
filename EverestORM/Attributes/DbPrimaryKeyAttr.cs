using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbPrimaryKeyAttr : Attribute
    {
        public DbPrimaryKeyAttr()
        {

        }
    }
}
