using Member.PYH._Scripts.Abstract;
using Member.PYH._Scripts.Interface;

namespace Member.PYH._Scripts.Item
{
    public class ItemAce : ItemBase, IUsable
    {
        protected override float Calculate(string expression)
        {
            return 0;
        }

        public void UseItem(string expression)
        {
        }
    }
}