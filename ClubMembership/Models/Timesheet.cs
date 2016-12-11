using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class Timesheet
    {
        public int TimeSheetId { get; set; }

        [Display(Name = "Record Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RecordStartDate { get; set; }

        [Display(Name = "Record End Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RecordEndDate { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0: mm}", ApplyFormatInEditMode = true)]
        public string StartTime
        {
            get { return RecordStartDate.ToString("hh:mm:ss", CultureInfo.CurrentCulture); ; }
        }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0: mm}", ApplyFormatInEditMode = true)]
        public string EndTime
        {
            get
            {
                
                    return RecordEndDate?.ToString("hh:mm:ss") ?? "pending";
                    
               
               
            }
        }

        public int MemberId { get; set; }

        [Display(Name = "Member")]
        public virtual Member Member { get; set; }
    }
}