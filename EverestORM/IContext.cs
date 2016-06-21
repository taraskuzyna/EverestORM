using System.Collections.Generic;

namespace EverestORM
{
    public interface IContext
    {
        /// <summary>
        /// Connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Read data set of output type
        /// </summary>
        /// <typeparam name="TOutput">Data set type</typeparam>
        /// <param name="where"> WHERE clauses of SQL query</param>
        /// <param name="param">Dictionary of SQL query parameters</param>
        /// <returns></returns>
        IEnumerable<TOutput> Select<TOutput>(string where = null, Dictionary<string, object> param = null) where TOutput : class, new();

        /// <summary>
        /// Read data set of output type from stored procedure
        /// </summary>
        /// <typeparam name="TOutput">Putput type</typeparam>
        /// <param name="procedure">Definition of stored procedure</param>
        /// <returns></returns>
        IEnumerable<TOutput> SelectProcedure<TOutput>(object procedure) where TOutput : class, new();

        /// <summary>
        /// Insert object into database
        /// </summary>
        /// <param name="obj">object FbTable</param>
        /// <returns>object id</returns>
        int Insert(object obj);

        /// <summary>
        /// Updates object on database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Update(object obj);

        /// <summary>
        /// Deletes object on database by ID
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="id"></param>
        void Delete<T>(object id) where T : class, new();

        /// <summary>
        /// Insert collection of objects
        /// </summary>
        /// <param name="collection">data to save</param>
        void BulkInsert<T>(IEnumerable<T> collection) where T : class, new();
    }
}
