using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }

        [Display(Name = "Payment Method Name")]
        public string Description { get; set; }

        }
}