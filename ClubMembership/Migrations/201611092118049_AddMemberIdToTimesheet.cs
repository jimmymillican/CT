namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberIdToTimesheet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timesheet", "Member_Id", "dbo.Member");
            DropIndex("dbo.Timesheet", new[] { "Member_Id" });
            RenameColumn(table: "dbo.Timesheet", name: "Member_Id", newName: "MemberId");
            AlterColumn("dbo.Timesheet", "MemberId", c => c.Int(nullable: true));
            CreateIndex("dbo.Timesheet", "MemberId");
            AddForeignKey("dbo.Timesheet", "MemberId", "dbo.Member", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheet", "MemberId", "dbo.Member");
            DropIndex("dbo.Timesheet", new[] { "MemberId" });
            AlterColumn("dbo.Timesheet", "MemberId", c => c.Int());
            RenameColumn(table: "dbo.Timesheet", name: "MemberId", newName: "Member_Id");
            CreateIndex("dbo.Timesheet", "Member_Id");
            AddForeignKey("dbo.Timesheet", "Member_Id", "dbo.Member", "Id");
        }
    }
}
