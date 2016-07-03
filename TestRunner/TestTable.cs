using EverestORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner
{
    [DbTableAttr("TEST_TABLE")]
    public class TestTable
    {
        [DbPrimaryKeyAttr]
        [DbColumnAttr("ID")]
        public int? Id { get; set; }

        [DbColumnAttr("INT1")]
        public int int1 { get; set; }
        [DbColumnAttr("INT2")]
        public int int2 { get; set; }
        [DbColumnAttr("INT3")]
        public int int3 { get; set; }
        [DbColumnAttr("INT4")]
        public int int4 { get; set; }
        [DbColumnAttr("INT5")]
        public int int5 { get; set; }

        [DbColumnAttr("ST1")]
        public string st1 { get; set; }
        [DbColumnAttr("ST2")]
        public string st2 { get; set; }
        [DbColumnAttr("ST3")]
        public string st3 { get; set; }
        [DbColumnAttr("ST4")]
        public string st4 { get; set; }
        [DbColumnAttr("ST5")]
        public string st5 { get; set; }
        [DbColumnAttr("ST6")]
        public string st6 { get; set; }
        [DbColumnAttr("ST7")]
        public string st7 { get; set; }
        [DbColumnAttr("ST8")]
        public string st8 { get; set; }
        [DbColumnAttr("ST9")]
        public string st9 { get; set; }
        [DbColumnAttr("ST10")]
        public string st10 { get; set; }

        [DbColumnAttr("DT1")]
        public DateTime dt1 { get; set; }
        [DbColumnAttr("DT2")]
        public DateTime dt2 { get; set; }
        [DbColumnAttr("DT3")]
        public DateTime dt3 { get; set; }
        [DbColumnAttr("DT4")]
        public DateTime dt4 { get; set; }
        [DbColumnAttr("DT5")]
        public DateTime dt5 { get; set; }

        [DbColumnAttr("DOB1")]
        public double dob1 { get; set; }
        [DbColumnAttr("DOB2")]
        public double dob2 { get; set; }
        [DbColumnAttr("DOB3")]
        public double dob3 { get; set; }
        [DbColumnAttr("DOB4")]
        public double dob4 { get; set; }
        [DbColumnAttr("DOB5")]
        public double dob5 { get; set; }

        public static List<TestTable> getData()
        {
            List<TestTable> list = new List<TestTable>();
            for (int i = 0; i < 500000; i++)
            {
                list.Add(new TestTable()
                {
                    int1 = i,
                    int2 = i,
                    int3 = i,
                    int4 = i,
                    int5 = i,
                    dt1 = DateTime.Now,
                    dt2 = DateTime.Now,
                    dt3 = DateTime.Now,
                    dt4 = DateTime.Now,
                    dt5 = DateTime.Now,
                    dob1 = i + 0.05,
                    dob2 = i + 0.05,
                    dob3 = i + 0.05,
                    dob4 = i + 0.05,
                    dob5 = i + 0.05,
                    st1 = DateTime.Now.ToLongTimeString(),
                    st2 = DateTime.Now.ToLongTimeString(),
                    st3 = DateTime.Now.ToLongTimeString(),
                    st4 = DateTime.Now.ToLongTimeString(),
                    st5 = DateTime.Now.ToLongTimeString(),
                    st6 = DateTime.Now.ToLongTimeString(),
                    st7 = DateTime.Now.ToLongTimeString(),
                    st8 = DateTime.Now.ToLongTimeString(),
                    st9 = DateTime.Now.ToLongTimeString(),
                    st10 = DateTime.Now.ToLongTimeString()
                });

            }
            return list;
        }
    }


}
