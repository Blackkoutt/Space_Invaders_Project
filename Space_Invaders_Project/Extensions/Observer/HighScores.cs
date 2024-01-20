using Space_Invaders_Project.Extensions.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class HighScores
    {
        private List<string> nicksList = new List<string>();
        private List<int> scoresList = new List<int>(); 
        private List<ISubscriber> subscribers = new List<ISubscriber>();
        private string nick = "test2";

        public HighScores() { } 


        // Metoda zapisująca tablice wyników do pliku
        public void SaveToFile()
        {
            string fileName = "highscores.txt";
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine("//\"nick score\"");
            for (int i =  0; i < nicksList.Count; i++) 
            {
                sw.WriteLine(nicksList[i] + " " + scoresList[i]);
            }
            sw.Close();
        }


        // Metoda odczytująca tablicę wyników z pliku - wyrzuca wyjątki w przypadku błędów związanych z odczytem
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
                throw new CannotFindFileException($"Cannot find file named: {fileName}");
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
                        throw new FileSyntaxException($"Bad syntax in: {fileName}");
                    }
                    nick = line.Substring(0, lastSpaceIndex).TrimEnd();
                    score = line.Substring(lastSpaceIndex + 1);
                    try
                    {
                        scoreInt = int.Parse(score);
                    }
                    catch
                    {
                        throw new CannotConvertException($"Failed to parse score value in: {fileName}");
                    }

                    nicksList.Add(nick);
                    scoresList.Add(scoreInt);
                }
            }
            sr.Close();
        }


        // Metoda informująca subskrybentów o zmianie stanu
        public void Notification(int score)
        {
            for (int i = 0; i < scoresList.Count; i++)
            {
                if (score > scoresList[i])
                {
                    foreach (ISubscriber subscriber in subscribers)
                    {
                        subscriber.Update(nick, score, i, ref nicksList, ref scoresList);
                    }
                    break;
                }
            }
        }


        // Metoda dodająca subskrybenta
        public void AddSubscriber(ISubscriber s)
        {
            subscribers.Add(s);
        }


        // Metoda usuwająca subskrybenta
        public void RemoveSubscriber(ISubscriber s)
        {
            subscribers.Remove(s);
        }


        // Metoda usuwająca wszytskich subskrybentów
        public void RemoveAllSubscribers()
        {
            subscribers.Clear();
        }


        // Gettery i settery
        public List<string> NickList
        {
            get { return nicksList; }
        }
        public List<int> ScoresList
        {
            get { return scoresList; }
        }
        public string Nick 
        {
            get { return nick; } 
            set { nick = value; } 
        }
    }
}
