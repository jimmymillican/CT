namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovenotnullfromEndDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Timesheet", "RecordEndDate");
            AddColumn("dbo.Timesheet", "RecordEndDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timesheet", "RecordEndDate");
        }
    }
}
