using System.Reflection;

namespace EverestORM.Model
{
    /// <summary>
    /// Definition of procedure input parameter
    /// </summary>
    public class DbParameter
    {
        /// <summary>
        /// Ordinal number of parameter
        /// </summary>
        public int OrdinalNumber { get; set; }

        /// <summary>
        /// Name of parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property mapped to parameter
        /// </summary>
        public PropertyInfo Property { get; set; }
    }
}
