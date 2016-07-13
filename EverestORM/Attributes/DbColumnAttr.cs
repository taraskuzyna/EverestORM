using System;

namespace EverestORM.Attributes
{
    /// <summary>
    /// Attribute for table column or procedure result column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttr : Attribute
    {
        private string name;

        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Default constructor. 
        /// In this case name of column equals name of property
        /// </summary>
        public DbColumnAttr()
        {
            this.name = String.Empty;
        }

        /// <summary>
        /// Constructor with specified column name
        /// </summary>
        /// <param name="name"></param>
        public DbColumnAttr(string name)
        {
            this.name = name;
        }
    }
}
