using System.Collections.Generic;

namespace sampleTriviaCsharp
{
    public class QuestionsResource
    {
        public string Language { get; set; }
        public string gameName { get; set; }
        public List<Questions> Questions { get; set; }
        public string HelpMessage { get; set; }
        public string RepeatQuestionMessage { get; set; }
        public string AskMessageStart { get; set; }
        public string HelpReprompt { get; set; }        
        public string CancelMessage { get; set; }
        public string NoMessage { get; set; }
        public string TriviaUnhandeled { get; set; }
        public string HelpUnhandeled { get; set; }
        public string StartGame { get; set; }
        public string NewGameMessage { get; set; }
        public string WelcomeMessage { get; set; }
        public string AnswerCorrectMessage { get; set; }
        public string AnswerWrongMessage { get; set; }
        public string CorrectAnswerMessage { get; set; }
        public string AnswerIsMessage { get; set; }
        public string TellQuestionMessage { get; set; }
        public string GameOverMessage { get; set; }
        public string ScoreIsMessage { get; set; }

        public QuestionsResource(string language)
        {
            Language = language;
            Questions = new List<Questions>();
        }
    }
}