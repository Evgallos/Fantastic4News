using Fantastic4News.Models.ViewModels;

namespace Fantastic4News.Services
{
	public interface IFileService
	{
        void UploadFileToContainer(string filePath, string fileName);

        void UploadFileToContainer2(string fileName, FileStream stream);
    }
}
