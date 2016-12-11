namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaign",
                c => new
                    {
                        CampaignId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        EditionId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignId)
                .ForeignKey("dbo.Edition", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
            CreateTable(
                "dbo.Edition",
                c => new
                    {
                        EditionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EditionId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Points = c.Int(nullable: false),
                        MembershipDate = c.DateTime(nullable: false),
                        MemberType = c.Int(nullable: false),
                        //DateOfBirth = c.DateTime(nullable: false),
                        //Address1 = c.String(),
                        //PostCode = c.String(),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAccount",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.CampaignMember",
                c => new
                    {
                        CampaignId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampaignId, t.MemberId })
                .ForeignKey("dbo.Campaign", t => t.CampaignId, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.CampaignId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CampaignMember", "MemberId", "dbo.Member");
            DropForeignKey("dbo.CampaignMember", "CampaignId", "dbo.Campaign");
            DropForeignKey("dbo.Campaign", "EditionId", "dbo.Edition");
            DropIndex("dbo.CampaignMember", new[] { "MemberId" });
            DropIndex("dbo.CampaignMember", new[] { "CampaignId" });
            DropIndex("dbo.Campaign", new[] { "EditionId" });
            DropTable("dbo.CampaignMember");
            DropTable("dbo.UserAccount");
            DropTable("dbo.Member");
            DropTable("dbo.Edition");
            DropTable("dbo.Campaign");
        }
    }
}
