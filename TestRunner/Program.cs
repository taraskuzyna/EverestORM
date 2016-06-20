using EverestORM.Attributes;
using EverestORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {

            IContext context = new FbContext("fb1");

            
            Dictionary<string, object> dict =  new Dictionary<string,object>();
            dict.Add("i", 1);
            List<Person> abc = context.Select<Person>("id = @i",dict).ToList();

            GetPersons get = new GetPersons() { P = "PPPPP", X = DateTime.Now };

            List<Person> abc1 = context.SelectProcedure<Person>(get).ToList();

            //int id = context.Insert(abc.First());

            context.BulkInsert(abc1);
        }
    }

    [DbTableAttr("PERSON")]
    public class Person
    {
        [DbPrimaryKeyAttr]
        [DbColumnAttr("ID")]
        public int Ref { get; set; }
        [DbColumnAttr]
        public string first_name { get; set; }
        [DbColumnAttr("DATE_OF_BIRTH")]
        public DateTime data { get; set; }

        public int MyProperty { get; set; }
    }
   
    [DbProcedureAttr("X_GET_PERSONS")]
    public class GetPersons
    {
        [DbParameterAttr(0)]
        public string P { get; set; }

        [DbParameterAttr(1)]
        public DateTime X { get; set; }

        public int MyProperty { get; set; }
    }

}
