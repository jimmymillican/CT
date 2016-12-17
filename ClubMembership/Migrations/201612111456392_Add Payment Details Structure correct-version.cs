namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentDetailsStructurecorrectversion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.MemberAccount",
                c => new
                    {
                        MemberAccountId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AccountTypeId_AccountTypeId = c.Int(),
                        MemberId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MemberAccountId)
                .ForeignKey("dbo.AccountType", t => t.AccountTypeId_AccountTypeId)
                .ForeignKey("dbo.Member", t => t.MemberId_Id)
                .Index(t => t.AccountTypeId_AccountTypeId)
                .Index(t => t.MemberId_Id);
            
            CreateTable(
                "dbo.MemberAccountPayment",
                c => new
                    {
                        MemberAccountPaymentId = c.Int(nullable: false, identity: true),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                        AdditionalDetails = c.String(),
                        AccountId_MemberAccountId = c.Int(),
                        MemberId_Id = c.Int(),
                        PaymentMethodId_PaymentMethodId = c.Int(),
                        PaymentStatusId_PaymentStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberAccountPaymentId)
                .ForeignKey("dbo.MemberAccount", t => t.AccountId_MemberAccountId)
                .ForeignKey("dbo.Member", t => t.MemberId_Id)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId_PaymentMethodId)
                .ForeignKey("dbo.PaymentStatus", t => t.PaymentStatusId_PaymentStatusId)
                .Index(t => t.AccountId_MemberAccountId)
                .Index(t => t.MemberId_Id)
                .Index(t => t.PaymentMethodId_PaymentMethodId)
                .Index(t => t.PaymentStatusId_PaymentStatusId);
            
            CreateTable(
                "dbo.PaymentMethod",
                c => new
                    {
                        PaymentMethodId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            CreateTable(
                "dbo.MemberPaymentMethod",
                c => new
                    {
                        MemberPaymentMethodId = c.Int(nullable: false, identity: true),
                        CardNumber = c.Int(nullable: false),
                        CardName = c.String(),
                        CardStartDate = c.String(),
                        CardExpiryDate = c.String(),
                        CardSecurityNumber = c.String(),
                        PaymentEmail = c.String(),
                        OtherDetails = c.String(),
                        MemberId_Id = c.Int(),
                        PaymentMethodId_PaymentMethodId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberPaymentMethodId)
                .ForeignKey("dbo.Member", t => t.MemberId_Id)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId_PaymentMethodId)
                .Index(t => t.MemberId_Id)
                .Index(t => t.PaymentMethodId_PaymentMethodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberPaymentMethod", "MemberId_Id", "dbo.Member");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountPayment", "MemberId_Id", "dbo.Member");
            DropForeignKey("dbo.MemberAccountPayment", "AccountId_MemberAccountId", "dbo.MemberAccount");
            DropForeignKey("dbo.MemberAccount", "MemberId_Id", "dbo.Member");
            DropForeignKey("dbo.MemberAccount", "AccountTypeId_AccountTypeId", "dbo.AccountType");
            DropIndex("dbo.MemberPaymentMethod", new[] { "PaymentMethodId_PaymentMethodId" });
            DropIndex("dbo.MemberPaymentMethod", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentStatusId_PaymentStatusId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentMethodId_PaymentMethodId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberAccountPayment", new[] { "AccountId_MemberAccountId" });
            DropIndex("dbo.MemberAccount", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberAccount", new[] { "AccountTypeId_AccountTypeId" });
            DropTable("dbo.MemberPaymentMethod");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.PaymentMethod");
            DropTable("dbo.MemberAccountPayment");
            DropTable("dbo.MemberAccount");
            DropTable("dbo.AccountType");
        }
    }
}
