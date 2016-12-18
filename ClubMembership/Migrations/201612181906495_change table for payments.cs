namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetableforpayments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberAccountPayment", "AccountId_MemberAccountId", "dbo.MemberAccount");
            DropForeignKey("dbo.MemberAccountPayment", "MemberId_Id", "dbo.Member");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.MemberPaymentMethod", "MemberId_Id", "dbo.Member");
            DropForeignKey("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod");
            DropIndex("dbo.MemberAccountPayment", new[] { "AccountId_MemberAccountId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentMethodId_PaymentMethodId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentStatusId_PaymentStatusId" });
            DropIndex("dbo.MemberPaymentMethod", new[] { "MemberId_Id" });
            DropIndex("dbo.MemberPaymentMethod", new[] { "PaymentMethodId_PaymentMethodId" });
            AddColumn("dbo.MemberAccountPayment", "AccountId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberAccountPayment", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberAccountPayment", "PaymentMethodId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberAccountPayment", "PaymentStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberPaymentMethod", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberPaymentMethod", "PaymentMethodId", c => c.Int(nullable: false));
            DropColumn("dbo.MemberAccountPayment", "AccountId_MemberAccountId");
            DropColumn("dbo.MemberAccountPayment", "MemberId_Id");
            DropColumn("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId");
            DropColumn("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId");
            DropColumn("dbo.MemberPaymentMethod", "MemberId_Id");
            DropColumn("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId", c => c.Int());
            AddColumn("dbo.MemberPaymentMethod", "MemberId_Id", c => c.Int());
            AddColumn("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId", c => c.Int());
            AddColumn("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId", c => c.Int());
            AddColumn("dbo.MemberAccountPayment", "MemberId_Id", c => c.Int());
            AddColumn("dbo.MemberAccountPayment", "AccountId_MemberAccountId", c => c.Int());
            DropColumn("dbo.MemberPaymentMethod", "PaymentMethodId");
            DropColumn("dbo.MemberPaymentMethod", "MemberId");
            DropColumn("dbo.MemberAccountPayment", "PaymentStatusId");
            DropColumn("dbo.MemberAccountPayment", "PaymentMethodId");
            DropColumn("dbo.MemberAccountPayment", "MemberId");
            DropColumn("dbo.MemberAccountPayment", "AccountId");
            CreateIndex("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId");
            CreateIndex("dbo.MemberPaymentMethod", "MemberId_Id");
            CreateIndex("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId");
            CreateIndex("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId");
            CreateIndex("dbo.MemberAccountPayment", "MemberId_Id");
            CreateIndex("dbo.MemberAccountPayment", "AccountId_MemberAccountId");
            AddForeignKey("dbo.MemberPaymentMethod", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod", "PaymentMethodId");
            AddForeignKey("dbo.MemberPaymentMethod", "MemberId_Id", "dbo.Member", "Id");
            AddForeignKey("dbo.MemberAccountPayment", "PaymentStatusId_PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId");
            AddForeignKey("dbo.MemberAccountPayment", "PaymentMethodId_PaymentMethodId", "dbo.PaymentMethod", "PaymentMethodId");
            AddForeignKey("dbo.MemberAccountPayment", "MemberId_Id", "dbo.Member", "Id");
            AddForeignKey("dbo.MemberAccountPayment", "AccountId_MemberAccountId", "dbo.MemberAccount", "MemberAccountId");
        }
    }
}
