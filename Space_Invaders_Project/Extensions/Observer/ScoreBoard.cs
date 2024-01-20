using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public class ScoreBoard : ISubscriber
    {
        public ScoreBoard() { } 
        public void Update(string name, int score, int position, ref List<string> nicks, ref List<int> scores)
        {
            nicks.Insert(position, name);
            scores.Insert(position, score);
            if(nicks.Count > 10)
            {
                nicks.RemoveAt(nicks.Count-1);
                scores.RemoveAt(scores.Count - 1);
            }
        }
    }
}
