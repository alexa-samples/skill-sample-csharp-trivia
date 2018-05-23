using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System.Text;
using AlexaAPI.Request;
using AlexaAPI.Response;
using AlexaAPI;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace sampleTriviaCsharp
{
    public class Function
    {
        private const string GAME_QUESTIONS_KEY = "gameQuestions";
        private const string ANSWERTEXT_KEY = "correctAnswerText";
        private const string SCORE = "score";
        private const string CORRECTANSWERINDEX = "correctAnswerIndex";
        private const string SPEECHOUTPUT_KEY = "speechOutput";
        private const string REPROMPT_KEY = "repromptText";
        private const string CURRENTQUESTION_KEY = "currentQuestionIndex";
        private const string STATE_KEY = "state";
        private const string ANSWER_SLOT = "Answer";
        private const string LOCALENAME = "LOCALE";
        private const string US_LOCALE = "en-US";

        private const int GAME_LENGTH = 5;
        private const int ANSWER_COUNT = 4;
        public static int currentScore = 0;

        private SkillResponse skillResponse = null;
        private ILambdaContext context = null;
        private static Random rand = new Random();

        public enum GAME_STATE
        {
            TRIVIA, // Asking trivia questions.
            START,  // Entry point, start the game.
            HELP    // The user is asking for help.
        }
        GAME_STATE gameState = GAME_STATE.START;

        /// <summary>
        /// A simple function that takes a SkillRequest and returns a SkillResponse along Session
        /// </summary>
        /// <param name="input">user request</param>
        /// <param name="ctx"></param>
        /// <returns>SkillResponse</returns>        
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext ctx)
        {
            context = ctx;
            skillResponse = new SkillResponse();
            skillResponse.Response = new ResponseBody();
            skillResponse.Version = "1.0";

            var locale = GetLocale(input);

            Resources allResources = new Resources();
            var resource = allResources.GetResources(locale).FirstOrDefault();

            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                GameStartHandler(true, resource);
            }
            else if (gameState == GAME_STATE.TRIVIA && input.Request.Type.Equals(AlexaConstants.IntentRequest))
            {
                GameStateHandler(input, input.Request, resource);
            }
            else if (gameState == GAME_STATE.HELP && input.Request.Type.Equals(AlexaConstants.IntentRequest))
            {
                HelpStateHandler(false, input, input.Request, resource);
            }

            return skillResponse;
        }

        /// <summary>
        ///  create a delegate response, we delegate all the dialog requests
        ///  except "Complete"
        /// </summary>
        /// <returns>void</returns>
        private void CreateDelegateResponse()
        {
            DialogDirective dld = new DialogDirective()
            {
                Type = AlexaConstants.DialogDelegate
            };
            skillResponse.Response.Directives.Add(dld);
        }

        /// <summary>
        /// Process any help request
        /// </summary>
        /// <param name="game"></param>
        /// <param name="skillinput"></param>
        /// <param name="intentrequest"></param>
        /// <param name="resource"></param>
        /// <returns>void</returns>
        private void GameStateHandler(SkillRequest input, Request intentRequest, QuestionsResource resource)
        {
            if (IsDialogIntentRequest(input))
            {
                if (!IsDialogSequenceComplete(input))
                {
                    // delegate to Alexa until dialog is complete
                    CreateDelegateResponse();
                    return;
                }
            }

            switch (input.Request.Intent.Name)
            {
                case AlexaConstants.StartOverIntent:
                    GameStartHandler(true, resource);
                    break;

                case AlexaConstants.RepeatIntent:
                    RebuildSession(input.Session, GAME_STATE.TRIVIA);
                    string reprompt = input.Session.Attributes[REPROMPT_KEY].ToString();
                    BuildSpeechResponse(resource.gameName, reprompt, reprompt, false);
                    break;

                case AlexaConstants.CancelIntent:                    
                case AlexaConstants.StopIntent:
                    BuildSpeechResponse(string.Empty, resource.CancelMessage, string.Empty, true);
                    break;

                case AlexaConstants.HelpIntent:
                    HelpStateHandler(false, input, input.Request, resource);
                    break;

                case "AnswerIntent":
                    HandleUserGuess(true, input, input.Request.Intent, resource);
                    break;

                case "DontKnowIntent":
                    HandleUserGuess(true, input, input.Request.Intent, resource);
                    break;

                default:
                    BuildSpeechResponse(string.Empty, resource.HelpReprompt, string.Empty, false);
                    break;
            }
        }

        /// <summary>
        /// Process any help request
        /// </summary>
        /// <param name="game"></param>
        /// <param name="skillinput"></param>
        /// <param name="intentrequest"></param>
        /// <param name="resource"></param>
        /// <returns>void</returns>
        private void HelpStateHandler(bool game, SkillRequest input, Request intentRequest, QuestionsResource resource)
        {
            Dictionary<string, object> sessionData = input.Session.Attributes;
            bool newGame = true;
            string speechOutput = string.Empty;
            bool endSession = false;

            switch (intentRequest.Intent.Name)
            {
                case AlexaConstants.StartOverIntent:
                    gameState = GAME_STATE.START;
                    GameStartHandler(false, resource);
                    return;

                case AlexaConstants.RepeatIntent:
                case AlexaConstants.HelpIntent:
                    if (sessionData != null &&
                        sessionData.ContainsKey("SPEECHOUTPUT_KEY") &&
                        sessionData.ContainsKey("REPROMPT_KEY"))
                    {
                        if (input.Session.Attributes[SPEECHOUTPUT_KEY].ToString() != null &&
                            input.Session.Attributes[REPROMPT_KEY].ToString() != null)
                        {
                            newGame = false;
                        }
                    }

                    HelpHandler(newGame, resource);
                    return;

                case AlexaConstants.StopIntent:
                case AlexaConstants.CancelIntent:
                    speechOutput = resource.CancelMessage;
                    endSession = true;
                    break;

                case AlexaConstants.SessionEndedRequest:
                    speechOutput = "Error in help state. Session ended";
                    endSession = true;
                    break;

                default:
                    speechOutput = resource.HelpUnhandeled;
                    break;
            }

            BuildSpeechResponse(string.Empty, speechOutput, string.Empty, endSession);
        }

        /// <summary>
        ///  configure and return a help message
        /// </summary>
        /// <param name="game"></param>
        /// <param name="resource"></param>
        /// <returns>void</returns>
        private void HelpHandler(bool game, QuestionsResource resource)
        {
            var askMessage = game ? resource.AskMessageStart : resource.RepeatQuestionMessage + resource.AskMessageStart;
            string speechOutput = string.Format(resource.HelpMessage, GAME_LENGTH) + askMessage;
            gameState = GAME_STATE.HELP;
            BuildSpeechResponse(string.Empty, speechOutput, string.Empty, false);
        }

        /// <summary>
        ///  handle a game start message and start the game
        /// </summary>
        /// <param name="game"></param>
        /// <param name="resource"></param>
        /// <returns>void</returns>
        private void GameStartHandler(bool newGame, QuestionsResource resource)
        {
            var message = string.Format(resource.NewGameMessage, resource.gameName)
                + string.Format(resource.WelcomeMessage, GAME_LENGTH.ToString());
            var speechOutput = newGame ? message : string.Empty;

            List<Questions> gameQuestions = GetGameQuestions(resource, false);

            int currentQuestionIndex = 0;
            int correctAnswerIndex = (rand.Next(1, ANSWER_COUNT)) - 1;
            var roundAnswers = GetShuffledAnswers(gameQuestions.ElementAt(currentQuestionIndex), currentQuestionIndex, correctAnswerIndex);
            string correctAnswerText = GetCorrectAnswer(roundAnswers, correctAnswerIndex);
            string reprompt = string.Format(resource.TellQuestionMessage, (currentQuestionIndex + 1).ToString()) +
                GetAllAnswersText(gameQuestions.ElementAt(currentQuestionIndex), roundAnswers, currentQuestionIndex);

            correctAnswerIndex++;
            speechOutput = speechOutput + reprompt;
            gameState = GAME_STATE.TRIVIA;

            skillResponse.SessionAttributes = new Dictionary<string, object>()
            {
                [SPEECHOUTPUT_KEY] = reprompt,
                [REPROMPT_KEY] = reprompt,
                [CURRENTQUESTION_KEY] = currentQuestionIndex,
                [CORRECTANSWERINDEX] = correctAnswerIndex,
                [GAME_QUESTIONS_KEY] = gameQuestions,
                [SCORE] = 0,
                [ANSWERTEXT_KEY] = correctAnswerText,
                [STATE_KEY] = gameState.ToString()
            };

            BuildSpeechResponse(resource.gameName, speechOutput, reprompt, false);
        }

        /// <summary>
        ///  handle a game start message and start the game
        /// </summary>
        /// <param name="userGaveUp"></param>
        /// <param name="input"></param>
        /// <param name="intent"></param>
        /// <param name="resource"></param>
        /// <returns>void</returns>
        private void HandleUserGuess(bool userGaveUp, SkillRequest input, Intent intent, QuestionsResource resource)
        {
            Dictionary<string, object> sessionData = input.Session.Attributes;

            var speechOutput = string.Empty;
            var speechOutputAnalysis = string.Empty;

            if (sessionData != null)
            {
                var gameQuestions = input.Session.Attributes[GAME_QUESTIONS_KEY];
                var correctAnswerIndex_previousQuestion = int.Parse(input.Session.Attributes[CORRECTANSWERINDEX].ToString());
                currentScore = int.Parse(input.Session.Attributes[SCORE].ToString());
                var currentQuestionIndex_previousQuestion = int.Parse(input.Session.Attributes[CURRENTQUESTION_KEY].ToString());
                var correctAnswerText = input.Session.Attributes[ANSWERTEXT_KEY];

                if (MatchUserGuessWithAnswer(intent, correctAnswerIndex_previousQuestion, resource))
                {
                    currentScore++;
                    speechOutputAnalysis = resource.AnswerCorrectMessage;
                }
                else
                {
                    if (!userGaveUp)
                    {
                        speechOutputAnalysis = resource.AnswerWrongMessage;
                    }
                    speechOutputAnalysis += string.Format(resource.CorrectAnswerMessage, correctAnswerIndex_previousQuestion, correctAnswerText);
                }

                // Check if we can exit the game session after GAME_LENGTH questions (zero-indexed)
                if (currentQuestionIndex_previousQuestion >= GAME_LENGTH - 1)
                {
                    speechOutput = userGaveUp ? string.Empty : resource.AnswerIsMessage;
                    speechOutput += speechOutputAnalysis + string.Format(resource.GameOverMessage, currentScore.ToString(), GAME_LENGTH.ToString());

                    BuildSpeechResponse(string.Empty, speechOutput, string.Empty, true);
                    return;
                }
                else
                {
                    var currentQuestionIndexNextQuestion = currentQuestionIndex_previousQuestion + 1;
                    var correctAnswerIndexNextQuestion = (rand.Next(1, ANSWER_COUNT)) - 1;

                    List<Questions> questions = JsonConvert.DeserializeObject<List<Questions>>(gameQuestions.ToString());
                    List<Questions> questionsList = new List<Questions>();

                    foreach (var item in questions)
                    {
                        questionsList.Add(new Questions()
                        {
                            Question = item.Question,
                            AnswerList = GetAnswersList(item.AnswerList)
                        });
                    }

                    var shuffledAnswers = GetShuffledAnswers(questionsList.ElementAt(currentQuestionIndexNextQuestion), currentQuestionIndexNextQuestion, correctAnswerIndexNextQuestion);
                    var correctAnswerText_nextQuestion = GetCorrectAnswer(shuffledAnswers, correctAnswerIndexNextQuestion);
                    string reprompt = string.Format(resource.TellQuestionMessage, (currentQuestionIndexNextQuestion + 1).ToString()) +
                        GetAllAnswersText(questionsList.ElementAt(currentQuestionIndexNextQuestion), shuffledAnswers, currentQuestionIndexNextQuestion);

                    speechOutput = userGaveUp ? string.Empty : resource.AnswerIsMessage;
                    speechOutput += speechOutputAnalysis + " " + string.Format(resource.ScoreIsMessage, currentScore.ToString()) + " " + reprompt;

                    correctAnswerIndexNextQuestion++; ;
                    gameState = GAME_STATE.TRIVIA;

                    skillResponse.SessionAttributes = new Dictionary<string, object>()
                    {
                        [SPEECHOUTPUT_KEY] = reprompt,
                        [REPROMPT_KEY] = reprompt,
                        [CURRENTQUESTION_KEY] = currentQuestionIndexNextQuestion,
                        [CORRECTANSWERINDEX] = correctAnswerIndexNextQuestion,
                        [GAME_QUESTIONS_KEY] = questionsList,
                        [SCORE] = currentScore,
                        [ANSWERTEXT_KEY] = correctAnswerText_nextQuestion,
                        [STATE_KEY] = gameState.ToString()
                    };

                    BuildSpeechResponse(resource.gameName, speechOutput, reprompt, false);
                    return;
                }
            }

            BuildSpeechResponse(string.Empty, AlexaConstants.ErrorMessage, string.Empty, false);
        }

        /// <summary>
        ///  get a list of answers
        /// </summary>
        /// <param name="answerList"></param>
        /// <returns>answerlist</returns>
        private List<Answers> GetAnswersList(List<Answers> answerList)
        {
            List<Answers> answers = new List<Answers>();
            foreach (var itemAnswer in answerList)
            {
                answers.Add(itemAnswer);
            }
            return answers;
        }

        /// <summary>
        ///  check if the users answer matches the correct answer
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="correctAnswerIndex"></param>
        /// <param name="resource"></param>
        /// <returns>true if a match fails otherwise</returns>
        private bool MatchUserGuessWithAnswer(Intent intent, int correctAnswerIndex, QuestionsResource resource)
        {
            Dictionary<string, Slot> slots = intent.Slots;
            double num;

            if (slots == null)
            {
                return false;
            }

            // Get the 'Answer' slot from the list slots.
            Slot answerSlot = slots[ANSWER_SLOT];

            if (answerSlot != null)
            {
                var answer = int.Parse(answerSlot.Value);
                bool flag = double.TryParse(answerSlot.Value, out num);

                // Check for answer and create output to user.
                if (flag)
                {
                    if (answer == correctAnswerIndex && answer <= ANSWER_COUNT && answer > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///  build the speech response
        /// </summary>
        /// <param name="title"></param>
        /// <param name="output"></param>
        /// <param name="reprompt"></param>
        /// <param name="shouldEndSession"></param>
        /// <returns>void</returns>
        private void BuildSpeechResponse(string title, string speechOutput, string reprompt, bool shouldEndSession)
        {
            if (title != string.Empty)
            {
                // Create the Simple card content.
                SimpleCard card = new SimpleCard();
                card.Title = title;
                card.Content = reprompt;
                skillResponse.Response.Card = card;
            }

            // create the speech response
            IOutputSpeech speechMessage = new PlainTextOutputSpeech();
            (speechMessage as PlainTextOutputSpeech).Text = speechOutput;

            if (reprompt != string.Empty)
            {
                PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
                speech.Text = reprompt;
                skillResponse.Response.Reprompt = new Reprompt();
                skillResponse.Response.Reprompt.OutputSpeech = speech;
            }            
            skillResponse.Response.OutputSpeech = speechMessage;
            skillResponse.Response.ShouldEndSession = shouldEndSession;
        }


        /// <summary>
        ///  GetGameQuestions
        /// </summary>
        /// <param name="resource, resource for current locale"></param>
        /// <param name="withPreface"></param>
        /// <returns>list of question</returns>
        private List<Questions> GetGameQuestions(QuestionsResource resource, bool withPreface)
        {
            // Pick GAME_LENGTH random questions from the list to ask the user, make sure there are no repeats.
            List<Questions> gameQuestions = new List<Questions>();
            var index = resource.Questions.Count;

            if (GAME_LENGTH > index)
            {
                throw new Exception("Invalid Game Length. " + index);
            }

            int i = 0;
            foreach (var question in resource.Questions.OrderBy(ans => rand.Next()))
            {
                gameQuestions.Add(question);
                i++;

                if (i == GAME_LENGTH)
                {
                    break;
                }
            }
            return gameQuestions;
        }

        /// <summary>
        ///  shuffle the answers 
        /// </summary>
        /// <param name="questions"></param>
        /// <param name="currentQuestionIndex"></param>
        /// <param name="correctAnswerTargetLocation"></param>
        /// <returns>string []</returns>
        private string[] GetShuffledAnswers(Questions questions, int currentQuestionIndex, int correctAnswerTargetLocation)
        {
            string[] answers = new string[ANSWER_COUNT];
            var answersCopy = questions.AnswerList.ToArray();
            var index = answersCopy.Length;

            if (index < ANSWER_COUNT)
            {
                throw new System.Exception("Not enough answers for question.");
            }

            // Shuffle the answers, excluding the first element which is the correct answer.
            for (var j = 1; j < answersCopy.Length; j++)
            {
                int randint = (int)Math.Floor((decimal)(rand.NextDouble() * (double)(index - 1))) + 1;
                index--;

                var answerCopy = answersCopy[index];
                answersCopy[index] = answersCopy[randint];
                answersCopy[randint] = answerCopy;
            }

            // Swap the correct answer into the target location
            for (var i = 0; i < ANSWER_COUNT; i++)
            {
                answers[i] = answersCopy[i].answerChoice;
            }

            var answerZero = answers[0];
            answers[0] = answers[correctAnswerTargetLocation];
            answers[correctAnswerTargetLocation] = answerZero;

            return answers.ToArray();
        }

        /// <summary>
        /// get all the answer text to the question list
        /// </summary>
        /// <param name="question"></param>
        /// <param name="shuffledAnswers"></param>
        /// <param name="currentQuestionIndex"></param>
        /// <returns>string of answers</returns>
        private string GetAllAnswersText(Questions question, string[] shuffledAnswers, int currentQuestionIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(question.Question + " ");

            int j = 0;
            foreach (var answers in shuffledAnswers)
            {
                sb.Append((j + 1).ToString() + ". " + answers + ". ");
                j++;

                if (j == ANSWER_COUNT)
                {
                    break;
                }
            }
            return sb.ToString();
        }

        private string GetCorrectAnswer(string[] roundAnswers, int correctAnswerIndex)
        {
            return roundAnswers.ElementAt(correctAnswerIndex);
        }

        private string GetLocale(SkillRequest input)
        {
            string locale = string.Empty;
            Dictionary<string, object> dictionary = input.Session.Attributes;

            if (dictionary != null)
            {
                if (dictionary.ContainsKey(LOCALENAME))
                {
                    locale = (string)dictionary[LOCALENAME];
                }
            }

            if (string.IsNullOrEmpty(locale))
            {
                locale = input.Request.Locale;
            }

            if (string.IsNullOrEmpty(locale))
            {
                locale = US_LOCALE;
            }

            skillResponse.SessionAttributes = new Dictionary<string, object>() { { LOCALENAME, locale } };
            return locale;
        }

        private void RebuildSession(Session session, GAME_STATE state)
        {
            Dictionary<string, object> sessionData = session.Attributes;

            if (sessionData == null)
            {
                return;
            }

            string speechOutput = session.Attributes[SPEECHOUTPUT_KEY].ToString();
            string reprompt = session.Attributes[REPROMPT_KEY].ToString();
            var gameQuestions = session.Attributes[GAME_QUESTIONS_KEY];
            var correctAnswerIndex = int.Parse(session.Attributes[CORRECTANSWERINDEX].ToString());       
            var currentQuestionIndex = int.Parse(session.Attributes[CURRENTQUESTION_KEY].ToString());
            var correctAnswerText = session.Attributes[ANSWERTEXT_KEY];

            currentScore = int.Parse(session.Attributes[SCORE].ToString());
            gameState = state;

            skillResponse.SessionAttributes = new Dictionary<string, object>
            {
                [SPEECHOUTPUT_KEY] = reprompt,
                [REPROMPT_KEY] = reprompt,
                [CURRENTQUESTION_KEY] = currentQuestionIndex,
                [CORRECTANSWERINDEX] = correctAnswerIndex,
                [GAME_QUESTIONS_KEY] = gameQuestions,
                [SCORE] = currentScore,
                [ANSWERTEXT_KEY] = correctAnswerText,
                [STATE_KEY] = gameState.ToString()
            };
        }

        /// <summary>
        /// logger interface
        /// </summary>
        /// <param name="text"></param>
        /// <returns>void</returns>
        private void Log(string text)
        {
            if (context != null)
            {
                context.Logger.LogLine(text);
            }
        }

        /// <summary>
        /// Check if its IsDialogIntentRequest, e.g. part of a Dialog sequence
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool true if a dialog</returns>
        private bool IsDialogIntentRequest(SkillRequest input)
        {
            return !string.IsNullOrEmpty(input.Request.DialogState);            
        }

        /// <summary>
        /// Check if its Dialog sequence is complete
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool true if dialog complete set</returns>
        private bool IsDialogSequenceComplete(SkillRequest input)
        {
            return !(input.Request.DialogState.Equals(AlexaConstants.DialogStarted) ||
               input.Request.DialogState.Equals(AlexaConstants.DialogInProgress));
        }
    }
}
