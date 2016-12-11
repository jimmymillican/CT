namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimesheetStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Timesheet",
                c => new
                    {
                        TimeSheetId = c.Int(nullable: false, identity: true),
                        RecordStartDate = c.DateTime(nullable: false),
                        RecordEndDate = c.DateTime(nullable: false),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.TimeSheetId)
                .ForeignKey("dbo.Member", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheet", "Member_Id", "dbo.Member");
            DropIndex("dbo.Timesheet", new[] { "Member_Id" });
            DropTable("dbo.Timesheet");
        }
    }
}
