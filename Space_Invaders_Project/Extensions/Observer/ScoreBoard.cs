using System;
using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        string[] tmpNicks = new string[HighScores.Nicks.Length];
        int[] tmpScores = new int[HighScores.Scores.Length];

        public ScoreBoard()
        {
            Array.Copy(HighScores.Nicks, tmpNicks, HighScores.Nicks.Length);
            Array.Copy(HighScores.Scores, tmpScores, HighScores.Scores.Length);
        } 
        public void Update(string name, int score, int position)
        {

            for (int i = 8; i >= position; i--)
            {
                tmpNicks[i+1] = HighScores.Nicks[i];
                tmpScores[i+1] = HighScores.Scores[i];
            }
            tmpNicks[position] = name;
            tmpScores[position] = score;
        }

        public void UpdateRealHighScores()
        {
            HighScores.Nicks = tmpNicks;
            HighScores.Scores = tmpScores;
            HighScores.SaveToFile();
        }
    }
}
