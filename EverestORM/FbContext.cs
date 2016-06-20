using EverestORM.Attributes;
using EverestORM.Model;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EverestORM
{
    /// <summary>
    /// Micro ORM engine 
    /// </summary>
    public class FbContext : IContext
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionName">Name of connection string</param>
        public FbContext(string connectionName)
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// Read data set of output type
        /// </summary>
        /// <typeparam name="TOutput">Data set type</typeparam>
        /// <param name="where"> WHERE clauses of SQL query</param>
        /// <param name="param">Dictionary of SQL query parameters</param>
        /// <returns></returns>
        public IEnumerable<TOutput> Select<TOutput>(string where = null, Dictionary<string, object> param = null) where TOutput : class, new()
        {
            DbTableAttr table = (DbTableAttr)Attribute.GetCustomAttributes(typeof(TOutput), false).FirstOrDefault();
            if (table == null)
                throw new InvalidOperationException("The provided object is not FbTable");

            List<FbParameter> list = new List<FbParameter>();
            if (param != null && param.Count > 0)
                foreach (var item in param)
                    list.Add(new FbParameter(item.Key, item.Value));

            string sql = String.Format("SELECT * FROM {0} WHERE {1}", table.Name, string.IsNullOrEmpty(where) ? "1=1" : where);
            DataTable dataTable = QueryToDataTable(sql, list);

            return DataTableToList<TOutput>(dataTable);
        }

        /// <summary>
        /// Read data set of output type from stored procedure
        /// </summary>
        /// <typeparam name="TOutput">Putput type</typeparam>
        /// <param name="procedure">Definition of stored procedure</param>
        /// <returns></returns>
        public IEnumerable<TOutput> SelectProcedure<TOutput>(object procedure) where TOutput : class, new()
        {
            DbProcedureAttr proc = (DbProcedureAttr)procedure.GetType().GetCustomAttributes(typeof(DbProcedureAttr), false).FirstOrDefault();
            if (proc == null)
                throw new InvalidOperationException("The provided object is not FbProcedure");

            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var prop in procedure.GetType().GetProperties())
            {
                DbParameterAttr attr = (DbParameterAttr)prop.GetCustomAttributes(typeof(DbParameterAttr), false).FirstOrDefault();
                if (attr == null)
                    continue;

                PropertyInfo propertyInfo = procedure.GetType().GetProperty(prop.Name);
                dict.Add(attr.OrdinalNumber.ToString(), prop.GetValue(procedure));
            }

            List<FbParameter> list = new List<FbParameter>();
            foreach (var item in dict.OrderBy(x => x.Key))
                list.Add(new FbParameter("p" + item.Key, item.Value));

            string sql = String.Format("SELECT * FROM {0}({1})", proc.Name, String.Join(",", list.Select(p => "@" + p.ParameterName).ToArray()));
            DataTable dataTable = QueryToDataTable(sql, list);

            return DataTableToList<TOutput>(dataTable);
        }

        /// <summary>
        /// Insert object into database
        /// </summary>
        /// <param name="obj">object FbTable</param>
        /// <returns>object id</returns>
        public int Insert(object obj)
        {
            DbTableAttr table = (DbTableAttr)obj.GetType().GetCustomAttributes(typeof(DbTableAttr), false).FirstOrDefault();
            if (table == null)
                throw new InvalidOperationException("The provided object is not FbTable");

            string primaryKey = null;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                DbColumnAttr attr = (DbColumnAttr)prop.GetCustomAttributes(typeof(DbColumnAttr), false).FirstOrDefault();
                if (attr == null)
                    continue;

                DbPrimaryKeyAttr pk = (DbPrimaryKeyAttr)prop.GetCustomAttributes(typeof(DbPrimaryKeyAttr), false).FirstOrDefault();
                if (pk != null)
                    primaryKey = string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name;


                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                dict.Add(string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name, prop.GetValue(obj));
            }

            List<FbParameter> list = new List<FbParameter>();
            foreach (var item in dict)
                list.Add(new FbParameter(item.Key, item.Value));

            string sql = String.Format("INSERT INTO {0} ({1}) VALUES ({2}) RETURNING {3}", table.Name,
                string.Join(", ", dict.Keys.ToArray()), "@" + string.Join(", @", dict.Keys.ToArray()), primaryKey);

            object o = ExecuteQueryScalar(sql, list);
            return Convert.ToInt32(o);
        }

        /// <summary>
        /// Updates object on database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(object obj)
        {
            DbTableAttr table = (DbTableAttr)obj.GetType().GetCustomAttributes(typeof(DbTableAttr), false).FirstOrDefault();
            if (table == null)
                throw new InvalidOperationException("The provided object is not FbTable");

            string primaryKey = null;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                DbColumnAttr attr = (DbColumnAttr)prop.GetCustomAttributes(typeof(DbColumnAttr), false).FirstOrDefault();
                if (attr == null)
                    continue;

                DbPrimaryKeyAttr pk = (DbPrimaryKeyAttr)prop.GetCustomAttributes(typeof(DbPrimaryKeyAttr), false).FirstOrDefault();
                if (pk != null)
                    primaryKey = string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name;


                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                dict.Add(string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name, prop.GetValue(obj));
            }

            List<FbParameter> list = new List<FbParameter>();
            foreach (var item in dict)
                list.Add(new FbParameter(item.Key, item.Value));

            string sql = String.Format("UPDATE {0} SET {2} WHERE {1} = @{1}", table.Name, primaryKey,
                string.Join(", ", dict.Keys.Select(k => k + " = @" + k).ToArray()));

            object o = ExecuteQueryScalar(sql, list);
            return true;
        }

        /// <summary>
        /// Deletes object on database by ID
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="id"></param>
        public void Delete<T>(object id) where T : class, new()
        {
            DbTableAttr table = (DbTableAttr)Attribute.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            if (table == null)
                throw new InvalidOperationException("The provided object is not FbTable");

            string primaryKey = null;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var prop in typeof(T).GetProperties())
            {
                DbColumnAttr attr = (DbColumnAttr)prop.GetCustomAttributes(typeof(DbColumnAttr), false).FirstOrDefault();
                if (attr == null)
                    continue;

                DbPrimaryKeyAttr pk = (DbPrimaryKeyAttr)prop.GetCustomAttributes(typeof(DbPrimaryKeyAttr), false).FirstOrDefault();
                if (pk != null)
                    primaryKey = string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name;
            }

            List<FbParameter> list = new List<FbParameter>();
            list.Add(new FbParameter(primaryKey, id));

            string sql = String.Format("DELETE FROM {0} WHERE {1} = @{1}", table.Name, primaryKey);

            ExecuteQueryScalar(sql, list);
        }

        /// <summary>
        /// Insert collection of objects
        /// </summary>
        /// <param name="collection">data to save</param>
        public void BulkInsert<T>(IEnumerable<T> collection) where T : class, new()
        {
            if (collection == null || !collection.Any())
                return;

            DbTable table = SchemeCache.GetTable(typeof(T), ConnectionString);

            string template = SqlTemlates.ExecuteBlock;
            string variable = @"a{0} TYPE OF COLUMN ""{1}"".""{2}"" = @a{0}";
            string insert = "INSERT INTO {0} ({1}) VALUES ({2});";

            List<FbParameter> parameters = new List<FbParameter>();
            List<string> variables = new List<string>();
            StringBuilder statements = new StringBuilder();

            int count = 0;
            int varCount = 0;
            Dictionary<string, string> help = new Dictionary<string, string>();
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();

                using (FbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (T item in collection)
                        {
                            help = new Dictionary<string, string>();

                            foreach (DbColumn col in table.Columns)
                            {
                                variables.Add(String.Format(variable, varCount, table.Name, col.Name));
                                parameters.Add(new FbParameter("a" + varCount, col.Property.GetValue(item)));
                                help.Add(col.Name, "a" + varCount);
                                varCount++;
                            }

                            statements.AppendLine(String.Format(insert, table.Name, String.Join(", ", help.Keys), ":" + String.Join(", :", help.Values)));
                            count++;

                            if (count == 255)
                            {
                                ExecuteBulkInsert(string.Format(template, String.Join(", \r\n", variables), statements.ToString()), parameters, connection, transaction);
                                count = 0;
                                varCount = 0;
                                parameters = new List<FbParameter>();
                                variables = new List<string>();
                                statements.Clear();
                            }
                        }

                        ExecuteBulkInsert(string.Format(template, String.Join(", \r\n", variables), statements.ToString()), parameters, connection, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Method to execute sql with input connection & transaction
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <param name="parameters">input parametrs</param>
        /// <param name="connection">input connection</param>
        /// <param name="transaction">input transaction</param>
        private void ExecuteBulkInsert(string sql, List<FbParameter> parameters, FbConnection connection, FbTransaction transaction)
        {
            FbCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.Transaction = transaction;
            command.Parameters.AddRange(parameters);
            command.ExecuteScalar();
        }

        /// <summary>
        /// Executes SQL query into DataTable
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="param">SQL query params</param>
        /// <returns>SQL query Result as DateTable</returns>
        internal DataTable QueryToDataTable(string sql, List<FbParameter> param)
        {
            DataTable dataTable = new DataTable();
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();

                using (FbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        FbCommand command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.Transaction = transaction;
                        if (param != null && param.Count > 0)
                            command.Parameters.AddRange(param);

                        using (FbDataAdapter dataAdapter = new FbDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTable);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error during executing query: " + sql, ex);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Executes SQL query scalar
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="param">SQL query params</param>
        /// <returns>object result</returns>
        private object ExecuteQueryScalar(string sql, List<FbParameter> param)
        {
            object obj = null;
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();

                using (FbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        FbCommand command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.Transaction = transaction;
                        if (param != null && param.Count > 0)
                            command.Parameters.AddRange(param);

                        obj = command.ExecuteScalar();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error during executing query: " + sql, ex);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// Converts DataTable to collection of output type 
        /// </summary>
        /// <typeparam name="TOutput">Output type</typeparam>
        /// <param name="table">Input DataTable</param>
        /// <returns>Collection of output type</returns>
        private List<TOutput> DataTableToList<TOutput>(DataTable table) where TOutput : class, new()
        {
            try
            {
                List<TOutput> collection = new List<TOutput>();

                foreach (var row in table.AsEnumerable())
                {
                    TOutput obj = new TOutput();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        DbColumnAttr attr = (DbColumnAttr)prop.GetCustomAttributes(typeof(DbColumnAttr), false).FirstOrDefault();
                        if (attr == null)
                            continue;
                        string column = "";
                        try
                        {
                            column = string.IsNullOrEmpty(attr.Name) ? prop.Name : attr.Name;
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[column], propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Invalid column name: " + column, ex);
                        }
                    }

                    collection.Add(obj);
                }

                return collection;
            }
            catch
            {
                return null;
            }
        }
    }
}
