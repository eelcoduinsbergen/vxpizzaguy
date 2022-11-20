using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace VX.PizzaGuy.SpeechEngine
{
    public class ChatBot
    {
        private const string KEY = "7c6a7fe1723e40b09b16b64e35d83bb0";
        private const string REGION = "northeurope";
        private SpeechConfig speechConfig;

        public ChatBot()
        {
            speechConfig = SpeechConfig.FromSubscription(KEY, REGION);            
        }

        public async Task Start()
        {
            await Intro();
            while(true)
            {
                await Conversation();
            }
        }

        private async Task Intro()
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig);

            var result = await speechSynthesizer.SpeakTextAsync("Hallo, wie is daar?");

        }

        private async Task Conversation()
        { 
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            Console.WriteLine("Speak into your microphone.");

            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            OutputSpeechRecognitionResult(speechRecognitionResult);

        }

        private void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
            }
        }


    }
}
