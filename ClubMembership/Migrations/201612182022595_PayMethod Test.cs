namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayMethodTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberAccountPayment", "Edition_EditionId", c => c.Int());
            CreateIndex("dbo.MemberAccountPayment", "Edition_EditionId");
            AddForeignKey("dbo.MemberAccountPayment", "Edition_EditionId", "dbo.Edition", "EditionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberAccountPayment", "Edition_EditionId", "dbo.Edition");
            DropIndex("dbo.MemberAccountPayment", new[] { "Edition_EditionId" });
            DropColumn("dbo.MemberAccountPayment", "Edition_EditionId");
        }
    }
}
