using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.Question;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Context
{
   public class TopLearnContext:DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options):base(options)
        {


        }

        #region User
        public  DbSet<User> Users { get; set; }
        public  DbSet<Role> Roles { get; set; }
        public  DbSet<UserInRole> UserInRoles { get; set; }

        public DbSet<UserDiscount> UserDiscount { get; set; }

        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        #endregion

        #region Permission
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Course
       public DbSet<CourseGroupe> CourseGroupes { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        public DbSet<CourseDiscount> CourseDiscounts { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }



        #endregion
        #region Question
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Answer> Answers{ get; set; }
        #endregion
        #region Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CourseGroupe>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Course>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CourseEpisode>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CourseComment>().HasQueryFilter(p => !p.IsRemoved);

            base.OnModelCreating(modelBuilder);
        }


    }
}
