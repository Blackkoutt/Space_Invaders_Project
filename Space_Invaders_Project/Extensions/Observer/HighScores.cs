using Space_Invaders_Project.Extensions.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Space_Invaders_Project.Extensions.Observer
{
    public static class HighScores
    {
        private static string[] nicks = new string[10];
        private static int[] scores = new int[10];
        private static List<ISubscriber> subscribers = new List<ISubscriber>();
        private static string nick;
        
        public static string[] Nicks { get { return nicks; } set { nicks = value; } }
        public static int[] Scores { get { return scores; } set { scores = value; } }

        public static void SaveToFile()
        {
            string fileName = "highscores.txt";
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine("//\"nick score\"");
            for (int i =  0; i < 10; i++) 
            {
                sw.WriteLine(nicks[i] + " " + scores[i]);
            }
            sw.Close();
        }

        //throw Exceptions
        public static void ReadFromFile()
        {
            StreamReader sr;
            string fileName = "highscores.txt";
            int lineNumber = 0;
            try
            {
                sr = new StreamReader(fileName);
            }
            catch (FileNotFoundException)
            {
                throw new CannotFindFileException($"Cannot find file named {fileName}");
            }
            catch(Exception ex)
            {
                throw new UnknownException($"Unknown error: {ex.Message}");
            }
            sr.ReadLine();
            string nick;
            string score;
            int scoreInt;
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                line = line.TrimStart();
                line = line.TrimEnd();
                if (!string.IsNullOrEmpty(line))
                {
                    int lastSpaceIndex = line.LastIndexOf(' ');
                    if(lastSpaceIndex == -1)
                    {
                        throw new FileSyntaxException($"Bad syntax in {fileName}");
                    }
                    nick = line.Substring(0, lastSpaceIndex).TrimEnd();
                    score = line.Substring(lastSpaceIndex + 1);
                    try
                    {
                        scoreInt = int.Parse(score);
                    }
                    catch
                    {
                        throw new CannotConvertException($"Failed to parse score value in {fileName}");
                    }
                    nicks[lineNumber] = nick;
                    scores[lineNumber++] = scoreInt;
                }
            }
            sr.Close();
        }
        public static void Notification(int score)
        {
            for (int i = 0; i < 10; i++)
            {
                if (score > scores[i])
                {
                    foreach (ISubscriber subscriber in subscribers)
                    {
                        subscriber.Update(nick, score, i);
                    }
                    break;
                }
            }
        }
        public static void AddSubscriber(ISubscriber s)
        {
            subscribers.Add(s);
        }
        public static void RemoveSubscriber(ISubscriber s)
        {
            subscribers.Remove(s);
        }
        public static void RemoveAllSubscribers()
        {
            subscribers.Clear();
        }
        public static string Nick 
        {
            get { return nick; } 
            set { nick = value; } 
        }

        public static void TestSC()
        {
            //--------------------------------------
            HighScores.ReadFromFile();
            HighScores.Nick = "zzzzzz";
            ScoreBoard sc = new ScoreBoard();
            HighScores.AddSubscriber(sc);
            HighScores.Notification(85);
            //--------------------------------------
        }
        public static void TestNF()
        {
            //--------------------------------------
            HighScores.ReadFromFile();
            HighScores.Nick = "zzzzzz";
            //Notification nf = new Notification();
            //HighScores.AddSubscriber(nf);
            HighScores.Notification(85);
            //--------------------------------------
        }
    }
}
