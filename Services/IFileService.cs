using Fantastic4News.Models.ViewModels;

namespace Fantastic4News.Services
{
	public interface IFileService
	{
        void UploadFileToContainer(string fileName, FileStream stream);
    }
}
