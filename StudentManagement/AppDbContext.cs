using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {

        }

        /// <summary>
        /// 重写种子
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public DbSet<Student> Students { get; set; }
    }
}
