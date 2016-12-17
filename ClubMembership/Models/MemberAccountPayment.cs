using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberAccountPayment
    {

        [Display(Name = "Payment Id")]
        public int MemberAccountPaymentId { get; set; }

        [Display(Name = "Account Id")]
        public virtual MemberAccount AccountId { get; set; }

        [Display(Name = "Member")]
        public virtual Member MemberId { get; set; }

        [Display(Name = "Payment Type")]
        public virtual PaymentMethod PaymentMethodId { get; set; }

        [Display(Name = "Payment Status")]
        public virtual PaymentStatus PaymentStatusId { get; set; }

        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float Amount { get; set; }

        [Display(Name = "Additional Details")]
        public string AdditionalDetails { get; set; }


    }
}