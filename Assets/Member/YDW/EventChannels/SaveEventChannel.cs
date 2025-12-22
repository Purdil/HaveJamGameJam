using Core;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    public enum SaveEventType
    {
        Save,
        Load
    }
    [CreateAssetMenu(fileName = "SaveEventChannel", menuName = "Event/SaveEvent", order = 0)]
    public class SaveEventChannel : EventChannel<SaveEventType>
    {
        
    }
}