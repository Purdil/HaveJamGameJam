using UnityEngine;

namespace Core.SaveSystem
{
    public class SaveId : ScriptableObject
    {
        public int id;
        public string saveName;
        [TextArea, SerializeField] public string saveDescription;
    }
}