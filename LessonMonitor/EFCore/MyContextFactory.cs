using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class MyContextFactory : IDesignTimeDbContextFactory<MyDBContext>
    {
        private const string connectionString = @"Data Source=ASHTON\ASHTON;Initial Catalog=LessonSql;Integrated Security=True;";

        public MyDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new MyDBContext(optionsBuilder.Options);
        }
    }
}
