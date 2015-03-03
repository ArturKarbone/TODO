using System.Data.Entity;
using TODO.Domain.Core.Entities;

namespace TODO.Data.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base("name=DataDbContext")
        {
            
        }

        public DbSet<Assignment> Assignments { get; set; }
    }
}
