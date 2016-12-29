namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelationshipType",
                c => new
                    {
                        RelationshipTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RelationshipTypeId);
            
            AddColumn("dbo.MemberAccountLinkedMember", "RelationshipTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.MemberAccountLinkedMember", "RelationshipTypeId");
            AddForeignKey("dbo.MemberAccountLinkedMember", "RelationshipTypeId", "dbo.RelationshipType", "RelationshipTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberAccountLinkedMember", "RelationshipTypeId", "dbo.RelationshipType");
            DropIndex("dbo.MemberAccountLinkedMember", new[] { "RelationshipTypeId" });
            DropColumn("dbo.MemberAccountLinkedMember", "RelationshipTypeId");
            DropTable("dbo.RelationshipType");
        }
    }
}
