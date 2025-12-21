using Core;
using UnityEngine;

namespace _01._SO.Events
{
    public enum SaveEventType
    {
        Save,
        Load
    }
    [CreateAssetMenu(fileName = "SaveEventChaanel", menuName = "Event/SaveEvent", order = 0)]
    public class SaveEventChannel : EventChannel<SaveEventType>
    {
        
    }
}