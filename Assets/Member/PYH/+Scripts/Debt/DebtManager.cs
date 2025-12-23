using System;
using Core;
using Core.SaveSystem;
using Member.YDW.EventChannels;
using UnityEngine;
using UnityEngine.Events;

namespace Member.PYH._Scripts.Debt
{
    public class DebtManager : MonoSingleton<DebtManager>, ISaveable
    {
        [Header("Save")]
        [SerializeField] private SaveEventChannel saveEventChannel;
        [SerializeField] private SaveId saveId;
        public SaveId SaveId => saveId;

        [SerializeField] private int maxRapay; // 주 당 갚아야하는 최대치
        [SerializeField] private int maxDebt;  // 총/남은 부채
        private int _currentDebt;              // 이번 주 갚아야 하는 부채
        private int _allWeekEndCount;          // 주차 카운트(변동)
        private int _debted;

        [SerializeField] private int defaultDay; // 주 길이
        private int _dayLeft;                   // 남은 날

        public UnityEvent gameEnd;
        public UnityEvent gameOver;

        private bool _suppressAutoSave;

        [Serializable]
        private struct DebtSavePayload
        {
            public int maxDebt;
            public int currentDebt;
            public int dayLeft;
            public int allWeekEndCount;
        }

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            _currentDebt = maxRapay;
            _dayLeft = defaultDay;

            _suppressAutoSave = true;
            RequestLoad();
            _suppressAutoSave = false;
        }

        private void RequestSave()
        {
            if (_suppressAutoSave) return;
            if (saveEventChannel == null) return;

            saveEventChannel.Raise(SaveEventType.Save);
        }
        private void RequestLoad()
        {
            if (saveEventChannel == null) return;

            saveEventChannel.Raise(SaveEventType.Load);
        }
        public string GetSaveData()
        {
            var payload = new DebtSavePayload
            {
                maxDebt = maxDebt,
                currentDebt = _currentDebt,
                dayLeft = _dayLeft,
                allWeekEndCount = _allWeekEndCount
            };

            return JsonUtility.ToJson(payload);
        }
        public void RestoreData(string loadData)
        {
            if (string.IsNullOrEmpty(loadData)) return;

            var payload = JsonUtility.FromJson<DebtSavePayload>(loadData);

            maxDebt = Mathf.Clamp(payload.maxDebt, 0, int.MaxValue);
            _dayLeft = Mathf.Clamp(payload.dayLeft, 0, defaultDay);
            _allWeekEndCount = Mathf.Max(0, payload.allWeekEndCount);

            _currentDebt = Mathf.Clamp(payload.currentDebt, 0, maxRapay);
            _currentDebt = Mathf.Min(_currentDebt, maxDebt);
        }
        public int GetCurrentDebt() => _currentDebt;
        public void DebtRepayment(int amount)
        {
            if (_currentDebt == 0) return;

            _currentDebt = Mathf.Clamp(_currentDebt - amount, 0, maxDebt);
            maxDebt = Mathf.Clamp(maxDebt - amount, 0, int.MaxValue);

            if (maxDebt == 0) gameEnd?.Invoke();
            if (_currentDebt <= 0)
            {
                _dayLeft = defaultDay;
                _currentDebt = GetPayThisPeriod(defaultDay, maxDebt, _debted);
                _debted++;
            }
            RequestSave();
        }

        public void NextDay()
        {
            _dayLeft = Mathf.Clamp(_dayLeft - 1, 0, defaultDay);

            if (_dayLeft == 0)
            {
                AllDayEndHandler();
            }

            RequestSave();
        }
        public void AllDayEndHandler()
        {
            gameOver?.Invoke();

            RequestSave();
        }
        public int GetAllWeekEnd() => _allWeekEndCount;
        public int GetDebted() => _debted;
        
        public int GetPayThisPeriod(int allDays, int maxDebt, int alldebted, float p = 1.5f)
        {
            if (allDays <= 0 || maxDebt <= 0) return 0;

            alldebted = Mathf.Clamp(alldebted, 0, allDays - 1);
            p = Mathf.Max(0.1f, p);

            double denom = 0.0;
            for (int k = 1; k <= allDays; k++)
                denom += Math.Pow(k, p);

            double numer = Math.Pow(alldebted + 1, p);
            double raw = maxDebt * (numer / denom);

            int pay = (int)Math.Floor(raw); // 정수화 방식(원하면 Round로 바꿔도 됨)
            return Mathf.Clamp(pay, 0, maxDebt);
        }
    }
}
