using UnityEngine;

namespace Member.PYH._Scripts.SO
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class SquareOperatorSO : OperatorSO
    {
        public override float Operation(float p1, float p2)
        {
            return Mathf.Pow(p1, p2);
        }
    }
}