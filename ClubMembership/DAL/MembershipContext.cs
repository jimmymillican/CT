using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClubMembership.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ClubMembership.DAL
{
    public class MembershipContext : DbContext
    {
        public MembershipContext() : base("MembershipContext")
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Timesheet> TimeSheets { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Campaign>()
               .HasMany(m => m.Members)
               .WithMany(c => c.Campaigns)
               .Map(cs =>
               {
                   cs.MapLeftKey("CampaignId");
                   cs.MapRightKey("MemberId");
                   cs.ToTable("CampaignMember");
               });
        }
    }
}