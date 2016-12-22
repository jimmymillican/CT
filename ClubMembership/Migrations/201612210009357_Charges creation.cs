namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chargescreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberAccountCharge",
                c => new
                    {
                        MemberAccountChargeId = c.Int(nullable: false, identity: true),
                        MemberAccountId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        ChargeDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MemberAccountChargeId)
                .ForeignKey("dbo.MemberAccount", t => t.MemberAccountId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId, cascadeDelete: true)
                .Index(t => t.MemberAccountId)
                .Index(t => t.PaymentMethodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberAccountCharge", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountCharge", "MemberAccountId", "dbo.MemberAccount");
            DropIndex("dbo.MemberAccountCharge", new[] { "PaymentMethodId" });
            DropIndex("dbo.MemberAccountCharge", new[] { "MemberAccountId" });
            DropTable("dbo.MemberAccountCharge");
        }
    }
}
