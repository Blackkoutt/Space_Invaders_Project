using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        public ScoreBoard() { } 
        public void Update(string name, int score, int position)
        {
            for (int i = 8; i >= position; i--)
            {
                HighScores.nicks[i+1] = HighScores.nicks[i];
                HighScores.scores[i+1] = HighScores.scores[i];
            }
            HighScores.nicks[position] = name;
            HighScores.scores[position] = score;
            HighScores.SaveToFile();
        }
    }
}
