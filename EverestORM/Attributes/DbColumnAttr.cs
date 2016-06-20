using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttr : Attribute
    {
        private string name;

        public string Name { get { return name; } }

        public DbColumnAttr()
        {
            this.name = String.Empty;
        }

        public DbColumnAttr(string name)
        {
            this.name = name;
        }
    }
}
