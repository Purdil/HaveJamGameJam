using Member.PYH._Scripts.Abstract;
using Member.PYH._Scripts.Interface;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.Item
{
    public class ItemHeal : ItemBase, IUsable
    {
        private bool _isActive;
        private int _count;
        
        private void OnEnable()
        {
            _isActive = false;
            _count = 3;
        }
        
        protected override float Calculate(string expression)
        {
            return 0;
        }
        public void UseItem(string expression)
        {
            float rend = Random.Range(0, 100);

            if (_isActive) { _count--; /* 플레이어 체력 회복 전역 이벤트를 여기서 호출 */ return;}
            if (_count == 0) { _isActive = false; _count = 3; return;}
            
            if (rend > item.ActiveProbability)
            {
                _isActive = true;
                // 플레이어 체력 회복 전역 이벤트를 여기서 호출
            }
        }
    }
}