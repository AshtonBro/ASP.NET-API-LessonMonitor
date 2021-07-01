using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.DataAccess
{
    public class SqlDbContextFactory : IDesignTimeDbContextFactory<SqlDBContext>
    {
        private const string connectionString = @"Data Source=ASHTON\ASHTON;Initial Catalog=LessonMonitorTestDb;Integrated Security=True;";

        public SqlDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlDBContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new SqlDBContext(optionsBuilder.Options);
        }
    }
}
