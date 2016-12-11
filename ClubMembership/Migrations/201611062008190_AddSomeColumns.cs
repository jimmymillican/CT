namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Member", "Address1", c => c.String());
            AddColumn("dbo.Member", "PostCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "PostCode");
            DropColumn("dbo.Member", "Address1");
            DropColumn("dbo.Member", "DateOfBirth");
        }
    }
}
