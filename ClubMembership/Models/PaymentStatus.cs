using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }

        [Display(Name = "Payment Status Name")]
        public string Description { get; set; }


    }
}