using System.Collections.Generic;
using Core;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Pieces
{
    public class PiecesManager : MonoSingleton<PiecesManager>
    { 
        private List<ItemSO> _pieces = new List<ItemSO>();
        [SerializeField] private int maxSlot;

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void TryAddItem(ItemSO item)
        {
            _pieces.Add(item);
        }
        public void TryRemoveItem(ItemSO item)
        {
            _pieces.Remove(item);
        }
    }
}
