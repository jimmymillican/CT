namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberAccountsLinkedMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberAccountLinkedMember",
                c => new
                    {
                        MemberAccountLinkedMemberId = c.Int(nullable: false, identity: true),
                        MemberAccountId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        AdditionalDetails = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MemberAccountLinkedMemberId)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: false)
                .ForeignKey("dbo.MemberAccount", t => t.MemberAccountId, cascadeDelete: false)
                .Index(t => t.MemberAccountId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberAccountLinkedMember", "MemberAccountId", "dbo.MemberAccount");
            DropForeignKey("dbo.MemberAccountLinkedMember", "MemberId", "dbo.Member");
            DropIndex("dbo.MemberAccountLinkedMember", new[] { "MemberId" });
            DropIndex("dbo.MemberAccountLinkedMember", new[] { "MemberAccountId" });
            DropTable("dbo.MemberAccountLinkedMember");
        }
    }
}
