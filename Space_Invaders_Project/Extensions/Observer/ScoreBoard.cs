using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        private List<string> nicksTemp;
        private List<int> scoresTemp;
        private List<string> nicksDefault;
        private List<int> scoresDefault;
        public ScoreBoard(List<string> nicks, List<int> scores) 
        {
            this.nicksTemp = nicks.GetRange(0, nicks.Count); //nicks;
            this.scoresTemp = scores.GetRange(0, scores.Count); //scores;
            this.nicksDefault = nicks.GetRange(0, nicks.Count); //nicks;
            this.scoresDefault = scores.GetRange(0, scores.Count); //scores;
        } 


        // Metoda aktualizująca tablicę wyników w czasie rzeczywistym 
        public void Update(string name, int score, int position)
        {
            for (int i = 8; i >= position; i--)
            {
                nicksTemp[i + 1] = nicksDefault[i];
                scoresTemp[i + 1] = scoresDefault[i];
            }
            nicksTemp[position] = name;
            scoresTemp[position] = score;
        }

        public void UpdateRealHighScores(ref List<string> nicks, ref List<int> scores)
        {
            nicks = this.nicksTemp;
            scores = this.scoresTemp;
        }
    }
}
