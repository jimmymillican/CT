namespace ClubMembership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayMethod : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.MemberAccountPayment", "MemberId", "dbo.Member", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MemberAccountPayment", "PaymentMethodId", "dbo.PaymentMethod", "PaymentMethodId", cascadeDelete: true);
            AddForeignKey("dbo.MemberAccountPayment", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
