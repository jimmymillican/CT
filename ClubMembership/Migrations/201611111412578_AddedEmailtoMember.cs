namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailtoMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Email");
        }
    }
}
