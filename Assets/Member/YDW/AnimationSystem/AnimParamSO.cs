using UnityEngine;

namespace Member.YDW.AnimationSystem
{
    [CreateAssetMenu(fileName = "AnimParam", menuName = "Animation/AnimParam", order = 0)]
    public class AnimParamSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public int HashValue { get; private set; }
        

        private void OnValidate()
        { 
            HashValue = Animator.StringToHash(Name);
        }
    }
}