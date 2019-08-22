using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace SqlAssembly
{
    public class Customer
    {
        [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillNameRow", TableDefinition = "Name nvarchar(max)")]
        public static IEnumerable GetCustomerNames()
        {
            return new List<string> { "Mark", "Jack", "Peter", "Henry", "Wood", "Mark"};
        }

        public static void FillNameRow(object name, out SqlString Name)
        {
            Name = (string)name;
        }
        
        //If the retunr type of the method is List of Objects then follow as below.
        //public static void FillNameRow(object objClass, out SqlString Name)
        //{
        //    BaseClass baseClass = (BaseClass)objClass;
        //    Name = baseClass.Name;
        //}
    }
}
