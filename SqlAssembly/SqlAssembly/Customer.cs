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
    }
}
