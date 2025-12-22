using System;
using System.Collections.Generic;
using System.Linq;
using _01._SO.Events;
using UnityEngine;

namespace Core.SaveSystem
{
    [Serializable]
    public struct SaveData
    {
        public int Id;
        public string JsonData;
    }

    [Serializable]
    public struct DataCollection
    {
        public List<SaveData> DataList;
    }
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private SaveEventChannel  saveEventChannel;
        [SerializeField] private string prefKey = "PrefSaveData";
        
        private List<SaveData> _unUsedData = new List<SaveData>();

        private void Awake()
        {
            saveEventChannel.OnEvent += HandleSaveEvent;
        }

        private void HandleSaveEvent(SaveEventType obj)
        {
            switch (obj)
            {
                case SaveEventType.Load:
                    HandlePrefLoadEvent();
                    break;
                case SaveEventType.Save:
                    HandlePrefSaveEvent();
                    break;
            }
        }


        private void HandlePrefSaveEvent()
        {
            string dataJson = GetDataToSave();
            PlayerPrefs.SetString(prefKey, dataJson);
            Debug.Log(dataJson);
        }

        private string GetDataToSave()
        {
            IEnumerable<ISaveable> saveableObjects
             = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveable>();
            
            List<SaveData> toSaveData = new List<SaveData>();
            foreach (ISaveable saveable in saveableObjects)
            {
                toSaveData.Add(new SaveData {Id = saveable.SaveId.id, JsonData = saveable.GetSaveData()});
            }
            toSaveData.AddRange(_unUsedData);
            DataCollection dataCollection = new DataCollection {DataList = toSaveData};
            
            return JsonUtility.ToJson(dataCollection);
        }
        
        [ContextMenu("Clear pref data")]
        private void ClearPrefData()
        => PlayerPrefs.DeleteKey(prefKey);
        private void HandlePrefLoadEvent()
        {
            // prefs 에서 데이터를 가져와서 그것을 
            // 로드 해주는 함수인RestoreData를 호출한다.
            RestoreData(PlayerPrefs.GetString(prefKey,string.Empty));
        }

        private void RestoreData(string loadJson)
        {
            IEnumerable<ISaveable> saveableObjects
                = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                    .OfType<ISaveable>();
            
            //씬안에 있는 모든 ISavable을 찾아와라.
            //로드된 데이터를 DataCollection으로 변경해라.
            DataCollection dataCollection = string.IsNullOrEmpty(loadJson)
            ?  new DataCollection() : JsonUtility.FromJson<DataCollection>(loadJson);
            // UnusedData는 지워놓고
            _unUsedData.Clear();
            if (dataCollection.DataList != null)
            {
                foreach (ISaveable saveable in saveableObjects)
                {
                    foreach (SaveData saveData in dataCollection.DataList)
                    {
                        if (saveData.Id == saveable.SaveId.id)
                        {
                            saveable.RestoreData(saveData.JsonData);
                        }
                        else
                        {
                            _unUsedData.Add(saveData);
                        }
                    }
                }
            }


            //로드된 DataCollection의 값들을 신안에 있는 ISavable 들한테 ID에 맞게 끼워서 복구해줘라.
            // 씬에 해당 ID가 존재하지 않으면 그 데이터는 UnUsed로 들어갑니다.
        }
    }
}