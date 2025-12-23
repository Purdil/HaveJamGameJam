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

        [SerializeField] private int defaultDay; // 주 길이
        private int _dayLeft;                   // 남은 날

        public UnityEvent allWeekEnd;
        public UnityEvent currentDebtAllRepayment;
        public UnityEvent gameEnd;

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
            if (_currentDebt <= 0) currentDebtAllRepayment?.Invoke();

            RequestSave();
        }

        public void NextDay()
        {
            _dayLeft = Mathf.Clamp(_dayLeft - 1, 0, defaultDay);

            if (_dayLeft == 0) allWeekEnd?.Invoke();

            RequestSave();
        }

        public void AllDayEndHandler()
        {
            _dayLeft = defaultDay;

            _allWeekEndCount++;
            _currentDebt = Mathf.Clamp(maxDebt / _dayLeft, 0, maxRapay);

            RequestSave();
        }

        public int GetAllWeekEnd() => _allWeekEndCount;
    }
}
