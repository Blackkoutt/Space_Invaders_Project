using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        public ScoreBoard() { } 
        public void Update(string name, int score, int position)
        {
            string[] nicks = HighScores.Nicks;
            int[] scores = HighScores.Scores;

            for (int i = 8; i >= position; i--)
            {
                nicks[i+1] = nicks[i];
                scores[i+1] = scores[i];
            }
            nicks[position] = name;
            scores[position] = score;
            HighScores.Nicks = nicks;
            HighScores.Scores = scores;
            HighScores.SaveToFile();
        }
    }
}
