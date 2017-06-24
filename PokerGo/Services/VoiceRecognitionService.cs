using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace PokerGo.Services
{
    public class VoiceRecognitionService
    {
        private SpeechRecognizer _speechRecognizer;
        private SpeechContinuousRecognitionSession RecongnitionSession { get; set; }

        public async Task Configure()
        {
            _speechRecognizer = new SpeechRecognizer();
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///PokerGoGrammar.grxml"));
            _speechRecognizer.Constraints.Add(new SpeechRecognitionGrammarFileConstraint(file));
            var result = await _speechRecognizer.CompileConstraintsAsync();

            RecongnitionSession = _speechRecognizer.ContinuousRecognitionSession;
            RecongnitionSession.AutoStopSilenceTimeout = TimeSpan.FromSeconds(2);
            RecongnitionSession.ResultGenerated += RecongnitionSessionOnResultGenerated;
            RecongnitionSession.Completed += RecongnitionSessionOnCompleted;
        }

        public async Task Enable()
        {
            await RecongnitionSession.StartAsync();
        }

        private void RecongnitionSessionOnCompleted(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            //
        }

        private void RecongnitionSessionOnResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            var keys = args.Result.SemanticInterpretation.Properties.Keys;
            var values = args.Result.SemanticInterpretation.Properties.Values;
        }

        public void Disable()
        {
            
        }
    }
}