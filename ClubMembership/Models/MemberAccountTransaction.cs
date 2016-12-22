using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberAccountTransaction
    { 
        [Display(Name = "Id")]
        public int MemberAccountTransactionId { get; set; }

        [Display(Name = "Account Id")]
        public int MemberAccountId { get; set; }
        
        public virtual MemberAccount MemberAccount { get; set; }

        [Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        [Display(Name = "Payment Type")]
        public int PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Payment Status")]
        public int PaymentStatusId { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }

        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float Amount { get; set; }

        [Display(Name = "Additional Details")]
        public string AdditionalDetails { get; set; }



    }
}