using System;

namespace EverestORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbProcedureAttr : Attribute
    {
        private string name;

        public string Name { get { return name; } }

        public DbProcedureAttr()
        {
            this.name = String.Empty;
        }

        public DbProcedureAttr(string name)
        {
            this.name = name;
        }
    }
}
