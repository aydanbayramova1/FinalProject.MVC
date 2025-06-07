using FinalProjectMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Scrolling> Scrollings { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Topbar> Topbars { get; set; }
        public DbSet<AboutBanner> AboutBanners { get; set; }
        public DbSet<BlogBanner> BlogBanners { get; set; }
        public DbSet<MenuBanner> MenuBanners { get; set; }
        public DbSet<TeamBanner> TeamBanners { get; set; }
        public DbSet<FaqsBanner> FaqsBanners { get; set; }
        public DbSet<ContactBanner> ContactBanners { get; set; }
        public DbSet<BlogDetailBanner> BlogDetailBanners { get; set; }
        public DbSet<ReservationBanner> ReservationBanners { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AboutRestaurant> AboutRestaurants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Approach> Approaches { get; set; }
        public DbSet<ApproachItem> ApproachItems { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<OurStory> OurStories { get; set; }
        public DbSet<StoryItem> StoryItems { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Approach>()
            .HasMany(a => a.Items)
            .WithOne(i => i.Approach)
            .HasForeignKey(i => i.ApproachId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OurStory>()
                .HasMany(s => s.StoryItems)
                .WithOne(i => i.OurStory)
                .HasForeignKey(i => i.OurStoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
