using Microsoft.EntityFrameworkCore;
using TaskPro_back.Models;
using static Azure.Core.HttpHeader;

namespace TaskPro_back
{
    public class TaskProContext : DbContext
    {
        public TaskProContext(DbContextOptions<TaskProContext> options) 
            : base(options)    
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set;}
        public DbSet<UserInTask> UsersInTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>( entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.Email);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired();

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .IsRequired();
            });

            modelBuilder.Entity<Models.Task>(entity => {
                entity.ToTable("tasks");

                entity.HasKey(e => e.id);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("is_completed")
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<UserInTask>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TaskId } );

                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();

                entity.Property(e => e.TaskId).HasColumnName("task_id").IsRequired();

                entity.Property(e => e.Role).HasColumnName("role").IsRequired();
            });

            base.OnModelCreating(modelBuilder); 
        }
    }
}
