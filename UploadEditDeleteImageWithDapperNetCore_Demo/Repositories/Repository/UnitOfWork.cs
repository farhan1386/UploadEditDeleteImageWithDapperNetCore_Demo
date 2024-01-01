using UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Interface;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISpeaker Speakers { get; set; }
        public UnitOfWork(ISpeaker Speakers)
        {
            this.Speakers = Speakers;
        }
    }
}
