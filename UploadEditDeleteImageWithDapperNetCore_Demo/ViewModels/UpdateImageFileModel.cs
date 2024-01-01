namespace UploadEditDeleteImageWithDapperNetCore_Demo.ViewModels
{
    public class UpdateImageFileModel:UploadImageFileViewModel
    {
        public Guid f_uid { get; set; }
        public string f_existing_photo { get; set; }
    }
}
