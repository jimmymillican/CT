namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredSuspendedandBlockedtodefaultto0 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberAccount", "Blocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MemberAccount", "Suspended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberAccount", "Suspended", c => c.Boolean());
            AlterColumn("dbo.MemberAccount", "Blocked", c => c.Boolean());
        }
    }
}
