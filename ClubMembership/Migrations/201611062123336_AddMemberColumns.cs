namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Member", "Telephone1", c => c.String());
            AddColumn("dbo.Member", "Telephone2", c => c.String());
            AddColumn("dbo.Member", "Expired", c => c.Boolean(nullable: false));
            AddColumn("dbo.Member", "ExpiryNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "ExpiryNotes");
            DropColumn("dbo.Member", "Expired");
            DropColumn("dbo.Member", "Telephone2");
            DropColumn("dbo.Member", "Telephone1");
            DropColumn("dbo.Member", "Gender");
        }
    }
}
