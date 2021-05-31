using Core;
using Core.Entities;
using Core.Entities.Base;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ToDoContext : DbContext
    {
        private readonly IHttpContextAccessor context;
        private readonly IConfiguration configuration;
        private readonly IMyAuthorizationServiceSingelton myAuthorizationSvc;

        public ToDoContext(
            IConfiguration configuration,
            IHttpContextAccessor context,
            IMyAuthorizationServiceSingelton myAuthorizationSvc,
            DbContextOptions<ToDoContext> options
            )
             : base(options)
        {
            this.configuration = configuration;
            this.context = context;
            this.myAuthorizationSvc = myAuthorizationSvc;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = this.configuration.GetConnectionString("default");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Tasks> Task { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            this.AddTimestamps();

            return await base.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            this.AddTimestamps();

            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            long userId;
            if (context.HttpContext.Request.Path.Value.Contains("sign-out"))
                userId = Constant.SystemId;
            else
                userId = myAuthorizationSvc.UserIdAuthenticated;

            var entities = this.ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedBy = userId;
                }

                if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                    ((BaseEntity)entity.Entity).UpdatedBy = userId;
                }
            }
        }
    }
}
