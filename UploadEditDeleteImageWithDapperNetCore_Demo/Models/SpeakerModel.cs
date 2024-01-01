using System.ComponentModel.DataAnnotations;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.Models
{
    public class SpeakerModel
    {
        public Guid f_uid { get; set; }
        [Display(Name = "ID")]
        public int f_iid { get; set; }

        [Display(Name = "First Name")]
        public string f_fname { get; set; }

        [Display(Name = "Last Name")]
        public string f_lname { get; set; }

        [Display(Name = "Gender")]
        public Gender f_gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Speach Date")]
        public DateTime f_speach_date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Speach Time")]
        public DateTime f_speach_time { get; set; }

        [Display(Name = "Venue")]
        public string? f_speach_venue { get; set; }

        [Display(Name = "Photo")]
        public string f_speaker_picture { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? f_create_date { get; set; }

        [Display(Name = "Update Date")]
        public DateTime? f_update_date { get; set; }

        [Display(Name = "Delete Date")]
        public DateTime? f_delete_date { get; set; }
    }
}
