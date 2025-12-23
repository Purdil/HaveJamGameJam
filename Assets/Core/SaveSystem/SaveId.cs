using UnityEngine;

namespace Core.SaveSystem
{
    [CreateAssetMenu(menuName = "Save/SaveId", fileName = "SaveId_")]
    public class SaveId : ScriptableObject
    {
        public int id;
        public string saveName;
        [TextArea, SerializeField] public string saveDescription;
    }
}