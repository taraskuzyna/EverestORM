using System;
using System.Collections.Generic;

namespace EverestORM.Model
{
    /// <summary>
    /// Definition of database stored procedure
    /// </summary>
    public class DbProcedure
    {
        /// <summary>
        /// Name of procedure
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Class mapped to this procedure
        /// </summary>
        public Type Class { get; set; }

        /// <summary>
        /// Collection of input parameters
        /// </summary>
        public List<DbParameter> Perameters { get; set; }
    }
}
