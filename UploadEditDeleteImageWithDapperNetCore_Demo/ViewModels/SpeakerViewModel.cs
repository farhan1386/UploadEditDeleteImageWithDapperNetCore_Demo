using System.ComponentModel.DataAnnotations;
using UploadEditDeleteImageWithDapperNetCore_Demo.Models;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.ViewModels
{
    public class SpeakerViewModel:UpdateImageFileModel
    {
        [Display(Name ="ID")]
        public int f_iid { get; set; }

        [Display(Name = "First Name")]
        public string f_fname { get; set; }

        [Display(Name = "Last Name")]
        public string f_lname { get; set; }

        [Display(Name = "Gender")]
        public Gender f_gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime f_speach_date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime f_speach_time { get; set; }

        [Display(Name = "Venue")]
        public string? f_speach_venue { get; set; }

        [Display(Name = "Create Date")]
        public DateTime f_create_date { get; set; }

        [Display(Name = "Update Date")]
        public DateTime f_update_date { get; set; }

        [Display(Name = "Delete Date")]
        public DateTime f_delete_date { get; set; }
    }
}
