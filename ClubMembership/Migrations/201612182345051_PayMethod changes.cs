namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayMethodchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberAccountPayment", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberAccount", "MemberId_Id", "dbo.Member");
            DropIndex("dbo.MemberAccount", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberId" });
            RenameColumn(table: "dbo.MemberAccount", name: "MemberId_Id", newName: "MemberId");
            AddColumn("dbo.MemberAccountPayment", "MemberAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.MemberAccount", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.MemberAccount", "MemberId");
            CreateIndex("dbo.MemberAccountPayment", "MemberAccountId");
            AddForeignKey("dbo.MemberAccountPayment", "MemberAccountId", "dbo.MemberAccount", "MemberAccountId", cascadeDelete: true);
            AddForeignKey("dbo.MemberAccount", "MemberId", "dbo.Member", "Id", cascadeDelete: true);
            DropColumn("dbo.MemberAccountPayment", "AccountId");
            DropColumn("dbo.MemberAccountPayment", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberAccountPayment", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberAccountPayment", "AccountId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MemberAccount", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberAccountPayment", "MemberAccountId", "dbo.MemberAccount");
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberAccountId" });
            DropIndex("dbo.MemberAccount", new[] { "MemberId" });
            AlterColumn("dbo.MemberAccount", "MemberId", c => c.Int());
            DropColumn("dbo.MemberAccountPayment", "MemberAccountId");
            RenameColumn(table: "dbo.MemberAccount", name: "MemberId", newName: "MemberId_Id");
            CreateIndex("dbo.MemberAccountPayment", "MemberId");
            CreateIndex("dbo.MemberAccount", "MemberId_Id");
            AddForeignKey("dbo.MemberAccount", "MemberId_Id", "dbo.Member", "Id");
            AddForeignKey("dbo.MemberAccountPayment", "MemberId", "dbo.Member", "Id", cascadeDelete: true);
        }
    }
}
