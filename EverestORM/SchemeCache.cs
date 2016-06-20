using EverestORM.Attributes;
using EverestORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EverestORM
{
    public static class SchemeCache
    {
        private static List<DbTable> cachedTables = new List<DbTable>();
        private static List<DbProcedure> cachedProcedures = new List<DbProcedure>();

        public static DbTable GetTable(Type table, string connectionString)
        {
            if (cachedTables.Any(t => t.Class.FullName.Equals(table.FullName) && t.Database.Equals(connectionString)))
                CacheTable(table, connectionString);
            return cachedTables.Where(t => t.Class.FullName == table.FullName).First();
        }

        public static DbProcedure GetProcedure(Type procedure)
        {
            if (cachedProcedures.Any(t => t.Class.FullName == procedure.FullName))
                CacheProcedure(procedure);
            return cachedProcedures.Where(t => t.Class.FullName == procedure.FullName).First();
        }

        private static void CacheTable(Type type, string connectionString)
        {
            DbTableAttr tableAttr = (DbTableAttr)type.GetCustomAttributes(typeof(DbTableAttr), false).FirstOrDefault();
            if (tableAttr == null)
                throw new ArgumentException("Input type has not DbTableAttr attribute");

            DbTable table = new DbTable()
            {
                Class = type,
                Name = tableAttr.Name.ToUpper(),
                Database = connectionString,
                Columns = new List<DbColumn>()
            };

            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (var prop in type.GetProperties())
            {
                DbColumnAttr attr = (DbColumnAttr)prop.GetCustomAttributes(typeof(DbColumnAttr), false).FirstOrDefault();
                if (attr == null)
                    continue;

                DbColumn column = new DbColumn();
                column.Name = String.IsNullOrEmpty(attr.Name) ? prop.Name.ToUpper() : attr.Name.ToUpper();
                column.Property = prop;
                table.Columns.Add(column);

                DbPrimaryKeyAttr pk = (DbPrimaryKeyAttr)prop.GetCustomAttributes(typeof(DbPrimaryKeyAttr), false).FirstOrDefault();
                if (pk != null)
                    table.PrimaryKey = column;
            }
            cachedTables.Add(table);
        }

        private static DbProcedure CacheProcedure(Type procedure)
        {
            throw new NotImplementedException();
        }
    }
}
