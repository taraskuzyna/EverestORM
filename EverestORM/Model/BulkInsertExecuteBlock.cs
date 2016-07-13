using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;

namespace EverestORM.Model
{
    /// <summary>
    /// Execute block for bulk insert engine
    /// </summary>
    public class BulkInsertExecuteBlock
    {
        private const int MaximumExecuteBlockQueries = 255;
        private const int MaximumExecuteBlockSize = 65535;
        private const int MaximumExecuteBlockInputParametersSize = 65535;

        private int currentBodySize = 0;
        private int currentInputParametersSize = 0;
        private int currentQueryCount = 0;

        /// <summary>
        /// Collection of parameters
        /// </summary>
        public List<FbParameter> Parameters { get; set; }

        /// <summary>
        /// Collection of variables
        /// </summary>
        public List<string> Variables { get; set; }

        /// <summary>
        /// Collection of statements
        /// </summary>
        public List<string> Statements { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BulkInsertExecuteBlock()
        {
            Parameters = new List<FbParameter>();
            Variables = new List<string>();
            Statements = new List<string>();
        }

        /// <summary>
        /// Method check if can add query to execute block
        /// </summary>
        /// <param name="query">new query</param>
        /// <returns></returns>
        public bool CanAddQuery(BulkInsertQuery query)
        {
            return currentQueryCount + 1 <= MaximumExecuteBlockQueries &&
                currentBodySize + query.Size + SqlTemlates.ExecuteBlock.Length <= MaximumExecuteBlockSize &&
                currentInputParametersSize + query.ParametersSize <= MaximumExecuteBlockInputParametersSize;
        }

        /// <summary>
        /// Method adds query to execute block
        /// </summary>
        /// <param name="query">new query</param>
        public void AddQuery (BulkInsertQuery query)
        {
            Statements.Add(query.Query);
            Variables.AddRange(query.Variables);
            Parameters.AddRange(query.Parameters);
            currentBodySize += query.Size + 4 * query.Variables.Count + 2;
            currentInputParametersSize += query.ParametersSize;
            currentQueryCount++;
        }

        /// <summary>
        /// Method build execute block
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(SqlTemlates.ExecuteBlock, String.Join(", \r\n", Variables), String.Join("\r\n", Statements));
        }
    }
}
