using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        private List<string> nicks;
        private List<int> scores;
        public ScoreBoard(List<string> nicks, List<int> scores) 
        {
            this.nicks = nicks.GetRange(0, nicks.Count);//nicks;
            this.scores = scores.GetRange(0, scores.Count);//scores;
        } 


        // Metoda aktualizująca tablicę wyników w czasie rzeczywistym 
        public void Update(string name, int score, int position)
        {
            nicks.Insert(position, name);
            scores.Insert(position, score);
            if(nicks.Count > 10)
            {
                nicks.RemoveAt(nicks.Count-1);
                scores.RemoveAt(scores.Count - 1);
            }
            List<string> aa = nicks;
            int b = 1;
        }

        public void UpdateRealHighScores(ref List<string> nicks, ref List<int> scores)
        {
            nicks = this.nicks;
            scores = this.scores;
            //nicks = this.nicks.GetRange(0, this.nicks.Count);//new List<string>(this.nicks);
            //this.nicks;
            //scores = this.scores.GetRange(0, this.scores.Count);//new List<int>(this.scores);
        }
    }
}
