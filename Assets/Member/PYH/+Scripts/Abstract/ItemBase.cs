using Core.Logger;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Abstract
{
    public abstract class ItemBase
    {
        [SerializeField] protected ItemSO item;
        
        protected char[] ToCharArray(string expression)
        {
            string replaced = expression.Replace(" ", "");
            char[] chars = replaced.ToCharArray();
            
            if (chars.Length > 5) { Logging.LogError("Wrong Expression"); return null;}
            return chars;
        }
        protected abstract float Calculate(string expression);
    }
}