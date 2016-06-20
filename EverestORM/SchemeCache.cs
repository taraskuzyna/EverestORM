using EverestORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestORM
{
    public static class SchemeCache
    {
        private static List<DbTable> cachedTables = new List<DbTable>();
        private static List<DbProcedure> cachedProcedures = new List<DbProcedure>();

        public static DbTable GetTable(Type table)
        {
            if (cachedTables.Any(t => t.Class.FullName == table.FullName))
                return cachedTables.Where(t => t.Class.FullName == table.FullName).First();
            else
                return CacheTable(table);
        }

        public static DbProcedure GetProcedure(Type procedure)
        {
            if (cachedProcedures.Any(t => t.Class.FullName == procedure.FullName))
                return cachedProcedures.Where(t => t.Class.FullName == procedure.FullName).First();
            else
                return CacheProcedure(procedure);
        }

        private static DbTable CacheTable(Type table)
        {
            throw new NotImplementedException();
        }

        private static DbProcedure CacheProcedure(Type procedure)
        {
            throw new NotImplementedException();
        }

    }
}
