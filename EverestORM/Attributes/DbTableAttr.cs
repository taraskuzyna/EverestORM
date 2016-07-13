using System;

namespace EverestORM.Attributes
{
    /// <summary>
    /// Attriburte for database table
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttr : Attribute
    {
        private string name;

        /// <summary>
        /// Name of table
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Default constructor.
        /// In this case table name equals class name
        /// </summary>
        public DbTableAttr()
        {
            this.name = String.Empty;
        }

        /// <summary>
        /// Constructor with specified table name
        /// </summary>
        /// <param name="name">table name</param>
        public DbTableAttr(string name)
        {
            this.name = name;
        }
    }
}
