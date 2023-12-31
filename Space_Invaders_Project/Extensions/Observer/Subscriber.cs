using System.Collections.Generic;

namespace Space_Invaders_Project.Extensions.Observer
{
    public interface Subscriber
    {
        void Update(string name, int score, Dictionary<string, int> scoreBoard);
    }
}
