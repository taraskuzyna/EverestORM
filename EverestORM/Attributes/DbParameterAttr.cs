using System;

namespace EverestORM.Attributes
{
    /// <summary>
    /// Attribute for procedure input parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttr : Attribute
    {
        private int ordinalNumber;
        private string name;

        /// <summary>
        /// Name of parameter
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Ordinal number of parametr
        /// </summary>
        public int OrdinalNumber { get { return ordinalNumber; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DbParameterAttr()
        {
            this.ordinalNumber = 0;
            this.name = String.Empty;
        }

        /// <summary>
        /// Constructor with specified ordinal number
        /// </summary>
        /// <param name="on">ordinal number</param>
        public DbParameterAttr(int on)
        {
            this.ordinalNumber = on;
            this.name = String.Empty;
        }

        /// <summary>
        /// Constructor with specified ordinal number and parameter name
        /// </summary>
        /// <param name="on">ordinal number</param>
        /// <param name="name">parameter name</param>
        public DbParameterAttr(int on, string name)
        {
            this.ordinalNumber = on;
            this.name = name;
        }
    }
}
