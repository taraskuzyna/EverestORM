using System.Reflection;

namespace EverestORM.Model
{
    public class DbColumn
    {
        public string Name { get; set; }

        public string DbType { get; set; }

        public int Size { get; set; }

        public PropertyInfo Property { get; set; }
    }
}
