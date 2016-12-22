namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedblockedsuspendednullables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberAccount", "Blocked", c => c.Boolean());
            AddColumn("dbo.MemberAccount", "BlockedDate", c => c.DateTime());
            AddColumn("dbo.MemberAccount", "Suspended", c => c.Boolean());
            AddColumn("dbo.MemberAccount", "SuspendedDate", c => c.DateTime());
            AlterColumn("dbo.MemberAccount", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberAccount", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.MemberAccount", "SuspendedDate");
            DropColumn("dbo.MemberAccount", "Suspended");
            DropColumn("dbo.MemberAccount", "BlockedDate");
            DropColumn("dbo.MemberAccount", "Blocked");
        }
    }
}
