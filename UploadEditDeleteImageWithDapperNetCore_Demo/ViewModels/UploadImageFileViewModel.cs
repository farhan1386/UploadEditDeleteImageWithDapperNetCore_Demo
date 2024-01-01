using System.ComponentModel.DataAnnotations;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.ViewModels
{
    public class UploadImageFileViewModel
    {
        [Display(Name = "Photo")]
        public IFormFile f_speaker_photo { get; set; }
    }
}
