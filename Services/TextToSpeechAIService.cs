using Fantastic4News.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Net;
using System.Text.RegularExpressions;

namespace Fantastic4News.Services
{
	public class TextToSpeechAIService: ITextToSpeechAiService
	{
		string subscriptionKey = "DLlfXEDJh9HqyS1Xl9oGJfHUwUCd5cuS43lRp4rMDfeXdlssu5P5JQQJ99BAACfhMk5XJ3w3AAAYACOGcWKZ";
		string subscriptionRegion = "swedencentral";

		

		public async Task<string> SynthesizeSpeech(string text)
		{
			// Decode HTML entities
			string decodedText = WebUtility.HtmlDecode(text);
			// Remove HTML tags
			string cleanText = Regex.Replace(decodedText, "<.*?>", string.Empty);
			// Replace multiple spaces, new lines, and tabs with a single space
			cleanText = Regex.Replace(cleanText, @"\s+", " ");
			cleanText.Trim();

			var config = SpeechConfig.FromSubscription(subscriptionKey, subscriptionRegion);
			config.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";
			using (var synthesizer = new SpeechSynthesizer(config))
			{
				var result = await synthesizer.SpeakTextAsync(cleanText);

				if (result.Reason == ResultReason.SynthesizingAudioCompleted)
				{
					return $"Speech synthesized for text";
				}
				else if (result.Reason == ResultReason.Canceled)
				{
					var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
					var errorMessage = $"CANCELED: Reason={cancellation.Reason}";

					if (cancellation.Reason == CancellationReason.Error)
					{
						errorMessage += $"\nCANCELED: ErrorCode={cancellation.ErrorCode}";
						errorMessage += $"\nCANCELED: ErrorDetails=[{cancellation.ErrorDetails}]";
						errorMessage += $"\nCANCELED: Did you update the subscription info?";
					}

					throw new System.Exception(errorMessage);
				}

				throw new System.Exception("Unknown error occurred.");
			}
		}
	}
}
