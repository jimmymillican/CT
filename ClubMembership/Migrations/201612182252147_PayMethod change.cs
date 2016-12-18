namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayMethodchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberAccountPayment", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.MemberAccountPayment", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.MemberAccountPayment", "MemberId", "dbo.Member");
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentStatusId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "PaymentMethodId" });
            DropIndex("dbo.MemberAccountPayment", new[] { "MemberId" });

            CreateIndex("dbo.MemberAccountPayment", "MemberId");
            CreateIndex("dbo.MemberAccountPayment", "PaymentMethodId");
            CreateIndex("dbo.MemberAccountPayment", "PaymentStatusId");
            
        }
        
        public override void Down()
        {
        
        }
    }
}
