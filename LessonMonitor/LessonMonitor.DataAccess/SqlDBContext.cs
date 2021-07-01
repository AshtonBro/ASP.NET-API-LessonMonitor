﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.DataAccess
{
    public class SqlDBContext : DbContext
    {
        public SqlDBContext(DbContextOptions<SqlDBContext> options) : base(options)
        {

        }

        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
