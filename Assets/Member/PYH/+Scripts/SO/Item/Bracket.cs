using Core.Logger;
using Member.PYH._Scripts.Interface;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BracketDataSO", menuName = "SO/ITEM/BracketDataSO")]
    public class Bracket : ItemSO, IUsable
    {
        public LayerMask Target { get; }
        
        public void UseItem()
        {
            Logging.Log("Use Bracket");
        }
    }
}
