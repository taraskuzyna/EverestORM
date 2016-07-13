using System.Reflection;

namespace EverestORM.Model
{
    /// <summary>
    /// Database column definition
    /// </summary>
    public class DbColumn
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column type
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// Column size in bytes
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Property mapped to this column
        /// </summary>
        public PropertyInfo Property { get; set; }
    }
}
