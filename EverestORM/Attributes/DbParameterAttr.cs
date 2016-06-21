using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttr : Attribute
    {
        private int no;
        private string name;

        public string Name { get { return name; } }

        public int OrdinalNumber { get { return no; } }

        public DbParameterAttr()
        {
            this.no = 0;
            this.name = String.Empty;
        }

        public DbParameterAttr(int no)
        {
            this.no = no;
            this.name = String.Empty;
        }

        public DbParameterAttr(int no, string name)
        {
            this.no = no;
            this.name = name;
        }
    }
}
