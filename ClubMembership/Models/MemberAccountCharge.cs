using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberAccountCharge
    {

        [Display(Name = "Charge Id")]
        public int MemberAccountChargeId { get; set; }

        [Display(Name = "Account Id")]
        public int MemberAccountId { get; set; }
        
        public virtual MemberAccount MemberAccount { get; set; }

        [Display(Name = "Agreed Payment Type")]
        public int PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Charge Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChargeDate { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float Amount { get; set; }

    }
}