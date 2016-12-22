namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changingpaymentstotransactions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberAccountPayment", "MemberAccountId", "dbo.MemberAccount");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentStatusId", "dbo.PaymentStatus");
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberAccountId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentMethodId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentStatusId" });
            CreateTable(
                "dbo.MemberAccountTransaction",
                c => new
                    {
                        MemberAccountTransactionId = c.Int(nullable: false, identity: true),
                        MemberAccountId = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        PaymentStatusId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                        AdditionalDetails = c.String(),
                    })
                .PrimaryKey(t => t.MemberAccountTransactionId)
                .ForeignKey("dbo.MemberAccount", t => t.MemberAccountId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentStatus", t => t.PaymentStatusId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionType", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.MemberAccountId)
                .Index(t => t.TransactionTypeId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.PaymentStatusId);
            
            CreateTable(
                "dbo.TransactionType",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            DropTable("dbo.MemberAccountPayment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MemberAccountPayment",
                c => new
                    {
                        MemberAccountPaymentId = c.Int(nullable: false, identity: true),
                        MemberAccountId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        PaymentStatusId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                        AdditionalDetails = c.String(),
                    })
                .PrimaryKey(t => t.MemberAccountPaymentId);
            
            DropForeignKey("dbo.MemberAccountTransaction", "TransactionTypeId", "dbo.TransactionType");
            DropForeignKey("dbo.MemberAccountTransaction", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.MemberAccountTransaction", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountTransaction", "MemberAccountId", "dbo.MemberAccount");
            DropIndex("dbo.MemberAccountTransaction", new[] { "PaymentStatusId" });
            DropIndex("dbo.MemberAccountTransaction", new[] { "PaymentMethodId" });
            DropIndex("dbo.MemberAccountTransaction", new[] { "TransactionTypeId" });
            DropIndex("dbo.MemberAccountTransaction", new[] { "MemberAccountId" });
            DropTable("dbo.TransactionType");
            DropTable("dbo.MemberAccountTransaction");
            CreateIndex("dbo.MemberAccountPayment", "PaymentStatusId");
            CreateIndex("dbo.MemberAccountPayment", "PaymentMethodId");
            CreateIndex("dbo.MemberAccountPayment", "MemberAccountId");
            AddForeignKey("dbo.MemberAccountPayment", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId", cascadeDelete: true);
            AddForeignKey("dbo.MemberAccountPayment", "PaymentMethodId", "dbo.PaymentMethod", "PaymentMethodId", cascadeDelete: true);
            AddForeignKey("dbo.MemberAccountPayment", "MemberAccountId", "dbo.MemberAccount", "MemberAccountId", cascadeDelete: true);
        }
    }
}
