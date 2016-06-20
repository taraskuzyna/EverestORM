using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttr : Attribute
    {
        private string name;

        public string Name { get { return name; } }

        public DbTableAttr()
        {
            this.name = String.Empty;
        }

        public DbTableAttr(string name)
        {
            this.name = name;
        }
    }
}
