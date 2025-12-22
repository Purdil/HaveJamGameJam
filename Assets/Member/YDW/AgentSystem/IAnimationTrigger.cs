using System;

namespace Member.YDW.AgentSystem
{
    public interface IAnimationTrigger
    {
        event Action OnAnimationEnd;
    }
}