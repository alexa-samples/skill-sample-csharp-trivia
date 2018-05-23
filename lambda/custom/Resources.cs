using System.Collections.Generic;

namespace sampleTriviaCsharp
{
    public class Resources
    {
        const string GB_LOCALE = "en-GB";
        const string US_LOCALE = "en-US";
        const string DE_LOCALE = "de-DE";

        public List<QuestionsResource> GetResources(string locale)
        {
            List<QuestionsResource> resources = new List<QuestionsResource>();

            switch (locale)
            {
                case GB_LOCALE:
                case US_LOCALE:
                    resources = getENResource(locale);
                    break;

                case DE_LOCALE:
                    resources = getDEResource();
                    break;

                default:
                    resources = getENResource(locale);
                    break;
            }
            return resources;
        }

        /*
        * When editing your questions pay attention to your punctuation. Make sure you use question marks or periods.
        * Make sure the first answer is the correct one. Set at least ANSWER_COUNT answers, any extras will be shuffled in.
        */

        public List<QuestionsResource> getENResource(string locale)
        {
            List<QuestionsResource> resource = new List<QuestionsResource>();
            QuestionsResource enGBResource = new QuestionsResource(locale);

            if (locale == GB_LOCALE)
            {
                enGBResource.gameName = "British Reindeer Trivia. "; // Be sure to change this for your skill.
            }
            else
            {
                enGBResource.gameName = "American Reindeer Trivia. "; // Be sure to change this for your skill.
            }

            enGBResource.HelpMessage = "I will ask you {0} multiple choice questions. Respond with the number of the answer. " +
            "For example, say one, two, three, or four. To start a new game at any time, say, start game. ";
            enGBResource.RepeatQuestionMessage = "To repeat the last question, say, repeat. ";
            enGBResource.AskMessageStart = "What would you like to do? ";
            enGBResource.HelpReprompt = "To give an answer to a question, respond with the number of the answer. ";
            enGBResource.CancelMessage = "Ok, let\'s play again soon. ";
            enGBResource.NoMessage = "Ok, we\'ll play another time. Goodbye! ";
            enGBResource.TriviaUnhandeled = "Try saying a number between 1 and {0} ";
            enGBResource.HelpUnhandeled = "Say start game to continue, or stop to end the game. ";
            enGBResource.StartGame = "Say start to start a new game. ";
            enGBResource.NewGameMessage = "Welcome to {0} ";
            enGBResource.WelcomeMessage = "I will ask you, {0} questions, try to get as many right as you can. " +
                            "Just say the number of the answer. Let\'s begin. ";
            enGBResource.AnswerCorrectMessage = " correct. ";
            enGBResource.AnswerWrongMessage = " wrong. ";
            enGBResource.CorrectAnswerMessage = "The correct answer is {0}, {1}. ";
            enGBResource.AnswerIsMessage = "That answer is ";

            enGBResource.TellQuestionMessage = "Question {0} : ";
            enGBResource.GameOverMessage = "You got {0} out of {1} questions correct. Thank you for playing! ";
            enGBResource.ScoreIsMessage = "Your score is {0}. ";

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Reindeer have very thick coats, how many hairs per square inch do they have?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "13,000" },
                    new Answers() { answerChoice = "1,200" },
                    new Answers() { answerChoice = "5,000" },
                    new Answers() { answerChoice = "700" },
                    new Answers() { answerChoice = "1,000" },
                    new Answers() { answerChoice = "120,000" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "The 1964 classic Rudolph The Red Nosed Reindeer was filmed in. ",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Japan" },
                    new Answers() { answerChoice = "United States" },
                    new Answers() { answerChoice = "Finland" },
                    new Answers() { answerChoice = "Germany" },
                    new Answers() { answerChoice = "Canada" },
                    new Answers() { answerChoice = "Norway" },
                    new Answers() { answerChoice = "France" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Santas reindeer are cared for by one of the Christmas elves, what is his name?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Wunorse Openslae" },
                    new Answers() { answerChoice = "Alabaster Snowball" },
                    new Answers() { answerChoice = "Bushy Evergreen" },
                    new Answers() { answerChoice = "Pepper Minstix" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "If all of Santas reindeer had antlers while pulling his Christmas sleigh, they would all be. ",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Girls" },
                    new Answers() { answerChoice = "Boys" },
                    new Answers() { answerChoice = "Girls and boys" },
                    new Answers() { answerChoice = "No way to tell" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "What do Reindeer eat?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Lichen" },
                    new Answers() { answerChoice = "Grasses" },
                    new Answers() { answerChoice = "Leaves" },
                    new Answers() { answerChoice = "Berries" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "What of the following is not true?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Caribou live on all continents" },
                    new Answers() { answerChoice = "Both reindeer and Caribou are the same species" },
                    new Answers() { answerChoice = "Caribou are bigger than reindeer" },
                    new Answers() { answerChoice = "Reindeer live in Scandinavia and Russia" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In what year did Rudolph make his television debut?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "1964" },
                    new Answers() { answerChoice = "1979" },
                    new Answers() { answerChoice = "2000" },
                    new Answers() { answerChoice = "1956" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Who was the voice of Rudolph in the 1964 classic?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Billie Mae Richards" },
                    new Answers() { answerChoice = "Burl Ives" },
                    new Answers() { answerChoice = "Paul Soles" },
                    new Answers() { answerChoice = "Lady Gaga" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In 1939 what retailer used the story of Rudolph the Red Nose Reindeer?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Montgomery Ward" },
                    new Answers() { answerChoice = "Sears" },
                    new Answers() { answerChoice = "Macys" },
                    new Answers() { answerChoice = "Kmart" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Santa\'s reindeer named Donner was originally named what?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Dunder" },
                    new Answers() { answerChoice = "Donny" },
                    new Answers() { answerChoice = "Dweedle" },
                    new Answers() { answerChoice = "Dreamy" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Who invented the story of Rudolph?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Robert May" },
                    new Answers() { answerChoice = "Johnny Marks"},
                    new Answers() { answerChoice = "Santa" },
                    new Answers() { answerChoice = "J.K. Rowling"}
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In what location will you not find reindeer?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "North Pole" },
                    new Answers() { answerChoice = "Lapland" },
                    new Answers() { answerChoice = "Korvatunturi mountain" },
                    new Answers() { answerChoice = "Finland" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "What Makes Santa\'s Reindeer Fly?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Magical Reindeer Dust" },
                    new Answers() { answerChoice = "Fusion" },
                    new Answers() { answerChoice = "Amanita muscaria" },
                    new Answers() { answerChoice = "Elves" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Including Rudolph, how many reindeer hooves are there?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "36" },
                    new Answers() { answerChoice = "24" },
                    new Answers() { answerChoice = "16" },
                    new Answers() { answerChoice = "8" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "Santa only has one female reindeer. Which one is it?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Vixen" },
                    new Answers() { answerChoice = "Clarice" },
                    new Answers() { answerChoice = "Cupid" },
                    new Answers() { answerChoice = "Burl" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 classic Rudolph The Red Nosed Reindeer, what was the snowman narrators name?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Sam" },
                    new Answers() { answerChoice = "Frosty" },
                    new Answers() { answerChoice = "Burl" },
                    new Answers() { answerChoice = "Snowy" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "What was Rudolph\'s father\'s name?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Donner" },
                    new Answers() { answerChoice = "Dasher" },
                    new Answers() { answerChoice = "Blixen" },
                    new Answers() { answerChoice = "Comet" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, What was the name of the coach of the Reindeer Games?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Comet" },
                    new Answers() { answerChoice = "Dasher" },
                    new Answers() { answerChoice = "Blixen" },
                    new Answers() { answerChoice = "Donner" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what is the name of the deer that Rudolph befriends at the reindeer games?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Fireball" },
                    new Answers() { answerChoice = "Clarice" },
                    new Answers() { answerChoice = "Jumper" },
                    new Answers() { answerChoice = "Vixen" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, How did Donner, Rudolph\'s father, try to hide Rudolph\'s nose?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Black mud" },
                    new Answers() { answerChoice = "Bag" },
                    new Answers() { answerChoice = "Pillow case" },
                    new Answers() { answerChoice = "Sock" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what does the Misfit Elf want to be instead of a Santa Elf?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Dentist" },
                    new Answers() { answerChoice = "Reindeer" },
                    new Answers() { answerChoice = "Toy maker" },
                    new Answers() { answerChoice = "Candlestick maker" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie,what was the Bumble\'s one weakness?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Could not swim" },
                    new Answers() { answerChoice = "Always Hungry" },
                    new Answers() { answerChoice = "Candy canes" },
                    new Answers() { answerChoice = "Cross eyed" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what is Yukon Cornelius really in search of?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Peppermint" },
                    new Answers() { answerChoice = "Gold" },
                    new Answers() { answerChoice = "India" },
                    new Answers() { answerChoice = "Polar Bears" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, why is the train on the Island of Misfit Toys?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Square wheels" },
                    new Answers() { answerChoice = "No Engine" },
                    new Answers() { answerChoice = "Paint does not match" },
                    new Answers() { answerChoice = "It does not toot" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what is the name of the Jack in the Box?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Charlie" },
                    new Answers() { answerChoice = "Sam" },
                    new Answers() { answerChoice = "Billy" },
                    new Answers() { answerChoice = "Jack" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, why did Santa Claus almost cancel Christmas?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Storm" },
                    new Answers() { answerChoice = "No snow" },
                    new Answers() { answerChoice = "No toys" },
                    new Answers() { answerChoice = "The Reindeer ere sick" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what animal noise did the elf make to distract the Bumble?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Oink" },
                    new Answers() { answerChoice = "Growl" },
                    new Answers() { answerChoice = "Bark" },
                    new Answers() { answerChoice = "Meow" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "In the 1964 movie, what is the name of the prospector?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Yukon Cornelius" },
                    new Answers() { answerChoice = "Slider Slam" },
                    new Answers() { answerChoice = "Bumble" },
                    new Answers() { answerChoice = "Jack" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "How far do reindeer travel when they migrate?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "3000 miles" },
                    new Answers() { answerChoice = "700 miles" },
                    new Answers() { answerChoice = "500 miles" },
                    new Answers() { answerChoice = "0 miles" }
                }
            });

            enGBResource.Questions.Add(new Questions()
            {
                Question = "How fast can a reindeer run?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "48 miles per hour" },
                    new Answers() { answerChoice = "19 miles per hour" },
                    new Answers() { answerChoice = "14 miles per hour" },
                    new Answers() { answerChoice = "52 miles per hour" },
                    new Answers() { answerChoice = "52 miles per hour" }
                }
            });

            resource.Add(enGBResource);
            return resource;
        }

        public List<QuestionsResource> getDEResource()
        {
            List<QuestionsResource> resource = new List<QuestionsResource>();
            /* ---------------------------------de-De-------------------------------------------*/

            QuestionsResource enDEResource = new QuestionsResource(DE_LOCALE);

            enDEResource.gameName = "Wissenswertes über Rentiere in Deutsch. "; // Be sure to change this for your skill.
            enDEResource.HelpMessage = "Ich stelle dir % s Multiple - Choice - Fragen.Antworte mit der Zahl, die zur richtigen Antwort gehört. " +
           "Sage beispielsweise eins, zwei, drei oder vier. Du kannst jederzeit ein neues Spiel beginnen, sage einfach „Spiel starten“. ";
            enDEResource.RepeatQuestionMessage = "Wenn die letzte Frage wiederholt werden soll, sage „Wiederholen“ ";
            enDEResource.AskMessageStart = "Du kannst jederzeit ein neues Spiel beginnen, sage einfach „Spiel starten“.";
            enDEResource.HelpReprompt = "Wenn du eine Frage beantworten willst, antworte mit der Zahl, die zur richtigen Antwort gehört. ";
            enDEResource.CancelMessage = "OK, dann lass uns bald mal wieder spielen.";
            enDEResource.NoMessage = "OK, spielen wir ein andermal. Auf Wiedersehen!";
            enDEResource.TriviaUnhandeled = "Sagt eine Zahl beispielsweise zwischen 1 und {0}";
            enDEResource.HelpUnhandeled = "Sage ja, um fortzufahren, oder nein, um das Spiel zu beenden.";
            enDEResource.StartGame = "Du kannst jederzeit ein neues Spiel beginnen, sage einfach „Spiel starten“.";
            enDEResource.NewGameMessage = "Willkommen bei {0} ";
            enDEResource.WelcomeMessage = "Ich stelle dir {0} Fragen und du versuchst, so viele wie möglich richtig zu beantworten. " +
            "Sage einfach die Zahl, die zur richtigen Antwort passt. Fangen wir an. ";
            enDEResource.AnswerCorrectMessage = " Richtig. ";
            enDEResource.AnswerWrongMessage = " Falsch. ";
            enDEResource.CorrectAnswerMessage = "Die richtige Antwort ist {0} {1}. ";
            enDEResource.AnswerIsMessage = "Diese Antwort ist ";

            enDEResource.TellQuestionMessage = "Frage {0} : ";
            enDEResource.GameOverMessage = "Du hast {0} von {1} richtig beantwortet. Danke fürs Mitspielen!";
            enDEResource.ScoreIsMessage = "Dein Ergebnis ist {0}. ";

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Rentiere haben ein sehr dickes Fell. Wie viele Haare pro Quadratzentimeter haben sie?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "13,000" },
                    new Answers() { answerChoice = "1,200" },
                    new Answers() { answerChoice = "5,000" },
                    new Answers() { answerChoice = "700"  },
                    new Answers() { answerChoice = "1,000" },
                    new Answers() { answerChoice = "120,000" }
               }
           });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Der Klassiker aus dem Jahr 1964, Rudolph mit der roten Nase, wurde gedreht in. ",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Japan" },
                    new Answers() { answerChoice = "USA" },
                    new Answers() { answerChoice = "Finnland" },
                    new Answers() { answerChoice = "Deutschland"  },
                    new Answers() { answerChoice = "Kanada" },
                    new Answers() { answerChoice = "Norwegen"},
                    new Answers() { answerChoice = "Frankreich"}
               }
           });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Um die Rentiere des Weihnachtsmanns kümmert sich eine der Weihnachtselfen. Wie heißt sie?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Wunorse Openslae" },
                    new Answers() { answerChoice = "Alabaster Snowball" },
                    new Answers() { answerChoice = "Bushy Evergreen" },
                    new Answers() { answerChoice = "Pfeffer Minstix"  }
               }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wenn alle Rentiere des Weihnachtsmanns Geweihe hätten, während sie seinen Weihnachtsschlitten ziehen, wären sie alle. ",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Weiblich" },
                    new Answers() { answerChoice = "Männlich" },
                    new Answers() { answerChoice = "Weiblich und männlich" },
                    new Answers() { answerChoice = "Kann man nicht sagen"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Was essen Rentiere?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Flechten" },
                    new Answers() { answerChoice = "Gras" },
                    new Answers() { answerChoice = "Blätter" },
                    new Answers() { answerChoice = "Beeren"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Welche Aussage ist nicht richtig?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Karibus leben auf allen Kontinenten" },
                    new Answers() { answerChoice = "Karibus und Rentiere gehören derselben Gattung an " },
                    new Answers() { answerChoice = "Karibus sind größer als Rentiere" },
                    new Answers() { answerChoice = "Rentiere leben in Skandinavien und Russland"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "In welchem Jahr kam Rudolph ins Fernsehen?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "1964" },
                    new Answers() { answerChoice = "1979" },
                    new Answers() { answerChoice = "2000" },
                    new Answers() { answerChoice = "1956"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wer war der Sprecher für Rudolph im klassischen Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Billie Mae Richards" },
                    new Answers() { answerChoice = "Burl Ives" },
                    new Answers() { answerChoice = "Paul Soles" },
                    new Answers() { answerChoice = "Lady Gaga"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Welche Handelskette verwendete 1939 die Geschichte von Rudolph mit der roten Nase?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Montgomery Ward" },
                    new Answers() { answerChoice = "Sears" },
                    new Answers() { answerChoice = "Macys" },
                    new Answers() { answerChoice = "Kmart"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie hieß das Rentier des Weihnachtsmanns namens Donner ursprünglich?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Dunder" },
                    new Answers() { answerChoice = "Donny" },
                    new Answers() { answerChoice = "Dweedle" },
                    new Answers() { answerChoice = "Dreamy"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wer hat die Geschichte von Rudolph erfunden?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Robert May" },
                    new Answers() { answerChoice = "Johnny Marks" },
                    new Answers() { answerChoice = "Santa" },
                    new Answers() { answerChoice = "J.K. Rowling"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wo findest du keine Rentiere?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Nordpol" },
                    new Answers() { answerChoice = "Lappland" },
                    new Answers() { answerChoice = "Korvatunturi-Berge" },
                    new Answers() { answerChoice = "Finnland"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Warum können die Rentiere des Weihnachtsmanns fliegen?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Magischer Staub der Rentiere" },
                    new Answers() { answerChoice = "Fusion" },
                    new Answers() { answerChoice = "Amanita muscaria" },
                    new Answers() { answerChoice = "Elfen"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wieviele Rentierhufe gibt es hier einschließlich Rudolph?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "36" },
                    new Answers() { answerChoice = "24" },
                    new Answers() { answerChoice = "16" },
                    new Answers() { answerChoice = "8"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Der Weihnachtsmann hat nur ein weibliches Rentier. Wie heißt es?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Blitzen" },
                    new Answers() { answerChoice = "Clarice" },
                    new Answers() { answerChoice = "Cupid" },
                    new Answers() { answerChoice = "Cupid"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie war der Name des erzählenden Schneemanns im klassischen Film Rudolph mit der roten Nase aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Sam" },
                    new Answers() { answerChoice = "Frosty" },
                    new Answers() { answerChoice = "Burl" },
                    new Answers() { answerChoice = "Snowy"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie hieß der Vater von Rudolph?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Donner" },
                    new Answers() { answerChoice = "Dasher" },
                    new Answers() { answerChoice = "Blixen" },
                    new Answers() { answerChoice = "Comet"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie war der Name des Trainers der Rentierspiele im klassischen Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Comet" },
                    new Answers() { answerChoice = "Dasher" },
                    new Answers() { answerChoice = "Blixen" },
                    new Answers() { answerChoice = "Donner"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie war im klassichen Film aus 1964 der Name des Hirsches, mit dem sich Rudolph befreundete?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Fireball" },
                    new Answers() { answerChoice = "Clarice" },
                    new Answers() { answerChoice = "Jumper" },
                    new Answers() { answerChoice = "Vixen"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie hat der Vater von Rudolph, Donner, im Film aus dem Jahr 1964 versucht, die Nase von Rudolph zu verbergen?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Schwarzer Schlamm" },
                    new Answers() { answerChoice = "Sack" },
                    new Answers() { answerChoice = "Kissenbezug" },
                    new Answers() { answerChoice = "Socke"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Was möchte die Misfit-Elfe im Film aus dem Jahr 1964 werden anstatt eine Elfe für den Weihnachtsmann?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Zahnarzt" },
                    new Answers() { answerChoice = "Rentier" },
                    new Answers() { answerChoice = "Spielzeugmacher" },
                    new Answers() { answerChoice = "Kerzenmacher"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Was war die einzige Schwäche von Bumble im Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Konnte nicht schwimmen" },
                    new Answers() { answerChoice = "War immer hungrig" },
                    new Answers() { answerChoice = "Zuckerstangen" },
                    new Answers() { answerChoice = "Schielte"   }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Was sucht Yukon Cornelius in Wirklichkeit im Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Pfefferminz" },
                    new Answers() { answerChoice = "Gold" },
                    new Answers() { answerChoice = "Indien" },
                    new Answers() { answerChoice = "Polarbären"  }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Warum befindet sich der Zug im Film aus dem Jahr 1964 auf der Insel des fehlerhaften Spielzeugs?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Viereckige Räder" },
                    new Answers() { answerChoice = "Keine Dampfmaschine" },
                    new Answers() { answerChoice = "Farbe stimmt nicht" },
                    new Answers() { answerChoice = "Pfeift nicht" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie lautet der Name des Schachtelmännchens im Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Charlie" },
                    new Answers() { answerChoice = "Sam" },
                    new Answers() { answerChoice = "Billy" },
                    new Answers() { answerChoice = "Jack" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Warum hat der Weihnachtsmann im Film aus dem Jahr 1964 Weihnachten beinahe abgesagt?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Sturm" },
                    new Answers() { answerChoice = "Kein Schnee" },
                    new Answers() { answerChoice = "Kein Spielzeug" },
                    new Answers() { answerChoice = "Die Rentiere waren krank" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Welches tierische Geräusch machte die Elfe im Film aus dem Jahr 1964, um den Bumble abzulenken?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Oink" },
                    new Answers() { answerChoice = "Knurr" },
                    new Answers() { answerChoice = "Wauwau" },
                    new Answers() { answerChoice = "Miau" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie lautet der Name des Goldsuchers im Film aus dem Jahr 1964?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "Yukon Cornelius" },
                    new Answers() { answerChoice = "Slider Slam" },
                    new Answers() { answerChoice = "Bumble" },
                    new Answers() { answerChoice = "Jack" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie weit ziehen Rentiere auf ihren Wanderungen?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "4800 km" },
                    new Answers() { answerChoice = "1100 km" },
                    new Answers() { answerChoice = "800 km" },
                    new Answers() { answerChoice = "0 km" }
                }
            });

            enDEResource.Questions.Add(new Questions()
            {
                Question = "Wie schnell läuft ein Rentier?",

                AnswerList = new List<Answers>()
                {
                    new Answers() { answerChoice = "77 km pro Stunde" },
                    new Answers() { answerChoice = "27 km pro Stunde" },
                    new Answers() { answerChoice = "30 km pro Stunde" },
                    new Answers() { answerChoice = "22 km pro Stunde" },
                    new Answers() { answerChoice = "83 km pro Stunde" },
                    new Answers() { answerChoice = "5 km pro Stunde" }
                }
            });

            resource.Add(enDEResource);
            return resource;
        }
    }
}