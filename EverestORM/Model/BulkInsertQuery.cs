using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverestORM.Model
{
    /// <summary>
    /// Insert query for bulk insert engine
    /// </summary>
    public class BulkInsertQuery
    {
        /// <summary>
        /// Collection of parameters
        /// </summary>
        public List<FbParameter> Parameters { get; set; }

        /// <summary>
        /// Collection of variables
        /// </summary>
        public List<string> Variables { get; set; }

        /// <summary>
        /// Insert query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Size of input paramerts collection
        /// </summary>
        public int ParametersSize { get; set; }

        /// <summary>
        /// Size of insert query and variables
        /// </summary>
        public int Size
        {
            get
            {
                return Variables.Sum(v => Encoding.UTF8.GetByteCount(v)) + Encoding.UTF8.GetByteCount(Query);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BulkInsertQuery()
        {
            Parameters = new List<FbParameter>();
            Variables = new List<string>();
        }
    }
}
