using Space_Invaders_Project.Extensions.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class HighScores
    {
        private Dictionary<string, int> scoreBoard;
        private List<Subscriber> subscribers;
        private string nick;
        public HighScores()
        {
            scoreBoard = new Dictionary<string, int>();
        }
        public void SaveToFile()
        {
            string fileName = "highscores.txt";
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine("//\"nick score\"");
            foreach (var item in scoreBoard)
            {
                sw.WriteLine(item.Key+" "+item.Value.ToString());
            }
            sw.Close();
        }

        //throw Exceptions
        public void ReadFromFile()
        {
            StreamReader sr;
            string fileName = "highscores.txt";
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
                    scoreBoard.Add(nick, scoreInt);
                }
            }
            int a = 1;
            sr.Close();
        }
        public void Notification(int score)
        {
            foreach (Subscriber subscriber in subscribers)
            {
                subscriber.Update(nick, score, scoreBoard);
            }
        }
        public void Subscribe(Subscriber s)
        {
            subscribers.Add(s);
        }
        public void Unsubscribe(Subscriber s)
        {
            subscribers.Remove(s);
        }
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }
    }
}
