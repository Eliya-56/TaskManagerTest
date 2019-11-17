using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Data
{
	public class TestTaskContext : DbContext
	{
		public DbSet<TestTaskDb> Tasks { get; set; }

		public TestTaskContext(DbContextOptions<TestTaskContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TestTaskDb>().HasKey(task => task.Id);
			base.OnModelCreating(modelBuilder);
		}
	}
}
