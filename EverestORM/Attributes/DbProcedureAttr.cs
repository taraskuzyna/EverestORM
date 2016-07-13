using System;

namespace EverestORM.Attributes
{
    /// <summary>
    /// Attribute for database procedures
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbProcedureAttr : Attribute
    {
        private string name;

        /// <summary>
        /// Name of procedure
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Default constructor.
        /// In this case procedure name equals class name
        /// </summary>
        public DbProcedureAttr()
        {
            this.name = String.Empty;
        }

        /// <summary>
        /// Constructor with specified procedure name
        /// </summary>
        /// <param name="name">procedure name</param>
        public DbProcedureAttr(string name)
        {
            this.name = name;
        }
    }
}
