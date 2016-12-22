namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAccountChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberAccount", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.MemberAccount", "AdditionalDetails", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberAccount", "AdditionalDetails");
            DropColumn("dbo.MemberAccount", "Amount");
        }
    }
}
