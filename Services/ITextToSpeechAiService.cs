using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Services
{
	public interface ITextToSpeechAiService
	{
		public Task<string> SynthesizeSpeech(string text);


	}
}
