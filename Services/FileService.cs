using Azure.Storage.Blobs;
using Fantastic4News.Models.ViewModels;


namespace Fantastic4News.Services
{
	public class FileService:IFileService
	{
		private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public void UploadFileToContainer(IFormFile model)

		{

			string connectionString = _configuration["AzureBlobConnectionString"];

			string containerName = _configuration["AzureBlobContainerName"];

			BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

			BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

			// Create the container if it does not exist

			containerClient.CreateIfNotExistsAsync();

			BlobClient blobClient = containerClient.GetBlobClient(model.FileName);

			using (var stream = model.OpenReadStream())

			{

				blobClient.Upload(stream, true);

			}

		}
	}
}
