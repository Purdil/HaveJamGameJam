using Core.Logger;
using Member.PYH._Scripts.Abstract;
using Member.PYH._Scripts.Interface;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.Item
{
    public class ItemSeven : ItemBase, IUsable
    {
        private int _count;
        
        private void OnDisable()
        {
            _count = 0;
        }
        protected override float Calculate(string expression)
        {
            return 0;
        }
        public void UseItem(string expression)
        {
            float rend = Random.Range(0, 100);

            if (rend > item.ActiveProbability)
            {
                _count++;

                if (_count >= 7)
                {
                    _count = 0;
                    Logging.Log("LUCK INCREASE + 7");
                }
            }
        }
    }
}
