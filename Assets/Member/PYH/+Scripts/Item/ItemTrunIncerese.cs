using Member.PYH._Scripts.Abstract;
using Member.PYH._Scripts.Interface;
using UnityEngine;

namespace Member.PYH._Scripts.Item
{
    public class ItemTrunIncerese : ItemBase, IUsable
    {
        private void OnDisable()
        {
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
                //
            }
        }
    }
}