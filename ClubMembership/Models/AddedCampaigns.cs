using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class AddedCampaigns
    {
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public bool Added { get; set; }
    }
}