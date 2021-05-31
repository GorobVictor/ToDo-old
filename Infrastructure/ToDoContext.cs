using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class ToDoContext : DbContext
    {
        private string ConnectionString { get; set; }

        public ToDoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
                optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
