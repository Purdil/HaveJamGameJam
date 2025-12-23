using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Member.PYH._Scripts.Debt
{
    public class DebtManager : MonoSingleton<DebtManager>
    {
        [SerializeField] private int maxRapay; // 주 당 갚아야하는 최대치
        [SerializeField] private int maxDebt; // 총 값아야하는 부채
        private int _currentDebt; // 현재  값아야하는 부채 (총 값아야하는 부채의 일부)
        private int _allWeekEndCount;

        [SerializeField] private int defaultDay; // 기본 부채 상환 날
        private int _dayLeft; // 부채 상환까지 현재 남은 날 (턴)
        
        public UnityEvent allWeekEnd;
        public UnityEvent currentDebtAllRepayment;
        public UnityEvent gameEnd;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _currentDebt = maxRapay;
            _dayLeft = defaultDay;
        }

        public int GetCurrentDebt() => _currentDebt;
        public void DebtRepayment(int amount)
        {
            if (_currentDebt == 0) return;
            
            _currentDebt = Mathf.Clamp(_currentDebt - amount, 0, maxDebt);
            maxDebt = Mathf.Clamp(maxDebt - amount, 0, int.MaxValue);
            
            if (maxDebt == 0)
            {
                gameEnd?.Invoke();
            }
            if (_currentDebt <= 0)
            {
                currentDebtAllRepayment?.Invoke();
            }
        }
        public void NextDay()
        {
            _dayLeft = Mathf.Clamp(_dayLeft - 1, 0, defaultDay);
            
            if (_dayLeft == 0)
            {
                allWeekEnd?.Invoke();
            }
        }
        public void AllDayEndHandler() // 일주일이 끝났을때 => 다시 재분배
        {
            _dayLeft = defaultDay;

            _allWeekEndCount++;
            _currentDebt = Mathf.Clamp(maxDebt / _dayLeft, 0, maxRapay);
        }

        public int GetAllWeekEnd()
        {
            return _allWeekEndCount;
        }
    }
}
