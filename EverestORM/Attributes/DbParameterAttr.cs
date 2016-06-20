using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttr : Attribute
    {
        private int no;

        public int OrdinalNumber { get { return no; } }

        public DbParameterAttr()
        {
            this.no = 0;
        }

        public DbParameterAttr(int no)
        {
            this.no = no;
        }
    }
}
