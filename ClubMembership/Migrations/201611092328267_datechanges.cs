namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datechanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Timesheet", "RecordEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timesheet", "RecordEndDate", c => c.DateTime(nullable: false));
        }
    }
}
