using System;
using System.Collections.Generic;

namespace EverestORM.Model
{
    /// <summary>
    /// Definition of databese table or view
    /// </summary>
    public class DbTable
    {
        /// <summary>
        /// Name of table
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Class mapped to this table
        /// </summary>
        public Type Class { get; set; }

        /// <summary>
        /// Table primary key column
        /// </summary>
        public DbColumn PrimaryKey { get; set; }

        /// <summary>
        /// Collection of table columns
        /// </summary>
        public List<DbColumn> Columns { get; set; }
    }
}
