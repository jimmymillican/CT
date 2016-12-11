namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedtoMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Deleted");
        }
    }
}
