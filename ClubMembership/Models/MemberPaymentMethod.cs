using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberPaymentMethod
    {
        public int MemberPaymentMethodId { get; set; }

        [Display(Name = "Member")]
        public virtual Member MemberId { get; set; }

        [Display(Name = "Payment Type")]
        public virtual PaymentMethod PaymentMethodId { get; set; }
         
        public int CardNumber { get; set; }

        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Start Date")]
        public string CardStartDate { get; set; }

        [Display(Name = "Card Expiry Date")]
        public string CardExpiryDate { get; set; }

        [Display(Name = "Card Security Number")]
        public string CardSecurityNumber { get; set; }

        [Display(Name = "Payment Email")]
        public string PaymentEmail { get; set; }

        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }

    }
}