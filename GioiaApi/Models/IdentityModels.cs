using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Gioia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace GioiaApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public string Fname { get; set; }


        public string Lname { get; set; }

      //  public int Age { get; set; }

        //[Required(ErrorMessage = "*")]
        public DateTime BirthDate { get; set; }

        public Byte[] Image { get; set; }
      //  public ICollection<Friend> Friends { get; set; }
      


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
      
        public static ApplicationDbContext Create()
        {
           
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
            .HasRequired(p => p.RequestedBy)
            .WithMany()
            .HasForeignKey(p => p.SenderId)
            .WillCascadeOnDelete(true);
            modelBuilder.Entity<Friend>()
                        .HasRequired(p => p.RequestedTo)
                        .WithMany()
                        .HasForeignKey(p => p.ReceiverId)
                        .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet <Posts> Posts { get; set; }

        public DbSet<userMood> UserMoods { get; set; }
        public DbSet<Photo> Photos { set; get; }
        public DbSet<Music> Musics { set; get; }
        public DbSet<Video> Videos { set; get; }

        //public DbSet<chats> chats { get; set; }

        public DbSet<Memos> Memos { get; set; }
        public DbSet<Sound> Sounds { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostComment> postComments { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<MessageGroup> UsMessageGroups { get; set; }

    }
}