namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTowntoMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Town", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Town");
        }
    }
}
