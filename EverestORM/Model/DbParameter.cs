using System.Reflection;

namespace EverestORM.Model
{
    public class DbParameter
    {
        public int OrdinalNumber { get; set; }

        public string Name { get; set; }

        public PropertyInfo Property { get; set; }
    }
}
