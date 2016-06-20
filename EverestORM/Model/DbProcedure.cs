using System;
using System.Collections.Generic;

namespace EverestORM.Model
{
    public class DbProcedure
    {
        public string Name { get; set; }

        public string Database { get; set; }

        public Type Class { get; set; }

        public List<DbParameter> Perameters { get; set; }
    }
}
