using EntityFrameworkExtras.EF6;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

namespace SP_UserDefinedTableTypeArg_objList
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiDbContext db = new ApiDbContext();
            var procedure = new TestStoredProcedure()
            {
                Customers = new List<CustTbl> { new CustTbl { Name = "Name1", Age = 22, Location = "London", SEX = "M" },
            new CustTbl {  Name = "Name2", Age = 23, Location = "Oxford", SEX = "M"} }
            };
            var result = db.Database.ExecuteStoredProcedure<CustTbl>(procedure);

            //Results with age as 27 and location as California for all Customers.
        }
    }
    
    public class ApiDbContext : DbContext
    {
        private static string connectionString = @"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = Customer; Integrated Security=true;";
        public ApiDbContext() : base(connectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApiDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// Define TestStoredProcedure stored procedure.
    /// </summary>
    [StoredProcedure("UpdateCust")]
    public class TestStoredProcedure
    { 

        [StoredProcedureParameter(SqlDbType.Udt, ParameterName = "tmp")]
        public List<CustTbl> Customers { get; set; }
    }

    [UserDefinedTableType("CustTblType")]
    public class CustTbl
    {
        [UserDefinedTableTypeColumn(1)]
        public string Name { get; set; }
        [UserDefinedTableTypeColumn(2)]
        public int Age { get; set; }
        [UserDefinedTableTypeColumn(3)]
        public string Location { get; set; }
        [UserDefinedTableTypeColumn(4)]
        public string SEX { get; set; }
    }
}

