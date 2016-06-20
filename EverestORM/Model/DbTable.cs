using System;
using System.Collections.Generic;
using System.Reflection;

namespace EverestORM.Model
{
    public class DbTable
    {
        public string Name { get; set; }

        public string Database { get; set; }

        public Type Class { get; set; }

        public DbColumn PrimaryKey { get; set; }

        public List<DbColumn> Columns { get; set; }
    }
}
