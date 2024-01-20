using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public interface ISubscriber
    {
        void Update(string name, int score, int position, ref List<string> nicks, ref List<int> scores);
    }
}
