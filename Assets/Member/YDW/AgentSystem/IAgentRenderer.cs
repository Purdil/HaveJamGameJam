using Member.YDW.AnimationSystem;
using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public interface IAgentRenderer
    {
        Sprite CurrentSprite { get; }
        void SetParam(AnimParamSO param, bool value);
        void SetParam(AnimParamSO param, int value);
        void SetParam(AnimParamSO param, float value);
        void SetParam(AnimParamSO param);
    }
}