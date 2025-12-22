using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.Abstract
{
    public abstract class ItemBase : MonoBehaviour
    {
        protected char[] ToCharArray(string expression)
        {
            string replaced = expression.Replace(" ", "");
            char[] chars = replaced.ToCharArray();
            
            if (chars.Length > 5) { Logging.LogError("Wrong Expression"); return null;}
            return chars;
        }
        protected virtual float Calculate(string expression)
        {
            Logging.LogError("Not Overridden");
            return 0;
        }
    }
}