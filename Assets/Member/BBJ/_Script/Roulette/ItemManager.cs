using BBJ;
using Core;
using JetBrains.Annotations;
using Member.PYH._Scripts.SO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoSingleton<ItemManager>
{
    [ReadOnly(true)]
    //public List<ItemSO> list = new();
    private List<ItemSO> _currentTrunUseItem = new();
    [SerializeField] private RouletEvent rouletStartChannel;
    [SerializeField] private RouletEvent rouletEndChannel;

    [SerializeField] private ItemListEvent itemSubChannel;
    [SerializeField] private ItemListEvent itemUnsubChannel;
    #region Event
    public event Action<Func<RouletNum>, Action<RouletNum>> OnAfter_RN_RN;
    public event Action<Func<RouletNum>, Action<ApplyFinal>> OnAfter_RN_AF;
    public event Action<Func<RouletNum>, Action<ApplyRouletNum>> OnAfter_RN_ARN;
    public event Action<Func<RouletNum>, Action<RouletOperator>> OnAfter_RN_RO;
    public event Action<Func<RouletOperator>, Action<RouletNum>> OnAfter_RO_RN;
    public event Action<Func<RouletOperator>, Action<ApplyFinal>> OnAfter_RO_AF;
    public event Action<Func<RouletOperator>, Action<ApplyRouletNum>> OnAfter_RO_ARN;
    public event Action<Func<RouletOperator>, Action<RouletOperator>> OnAfter_RO_RO;

    public event Action<Action<RouletNum>> OnBefore_RouletNum;
    public event Action<Action<ApplyFinal>> OnBefore_ApplyFinal;
    public event Action<Action<ApplyRouletNum>> OnBefore_ApplyRouletNum;
    public event Action<Action<RouletOperator>> OnBefore_RouletOperator;
    #endregion

    public void Start()
    {
        rouletStartChannel.OnEvent += OnRouletBefore;
        rouletEndChannel.OnEvent += OnRouletAfter;
        itemSubChannel.OnEvent += ResetSpine;
        itemUnsubChannel.OnEvent += EndTrun;
    }
    public void OnDestroy()
    {
        rouletStartChannel.OnEvent -= OnRouletBefore;
        rouletEndChannel.OnEvent -= OnRouletAfter;
        itemSubChannel.OnEvent -= ResetSpine;
        itemUnsubChannel.OnEvent -= EndTrun;
    }
    public void EndTrun(List<ItemSO> _) {if (_ == null) return; UnSubItems(); }
    public void ResetSpine(List<ItemSO> list)
    {
        if (list == null) return;

        _currentTrunUseItem.Clear();
        foreach (var item in list)
        {
            if (item is IProbabilityItem probability)
            {
                if (probability.Probability == 0) continue;
                if (probability.Probability <= Random.value * 100) continue;
            }
            SubEvent(item);
            _currentTrunUseItem.Add(item);
        }
    }
    public void SubEvent(ItemSO item)
    {
        if (item is IAfterApplyTrunIteem<RouletNum, RouletNum> a1) OnAfter_RN_RN += a1.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, ApplyFinal> a2) OnAfter_RN_AF += a2.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, ApplyRouletNum> a3) OnAfter_RN_ARN += a3.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, RouletOperator> a4) OnAfter_RN_RO += a4.AfterApply;

        if (item is IAfterApplyTrunIteem<RouletOperator, RouletNum> a5) OnAfter_RO_RN += a5.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, ApplyFinal> a6) OnAfter_RO_AF += a6.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, ApplyRouletNum> a7) OnAfter_RO_ARN += a7.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, RouletOperator> a8) OnAfter_RO_RO += a8.AfterApply;

        if (item is IBeforeApplyTrunItem<RouletNum> c1) OnBefore_RouletNum += c1.BeforeApply;
        if (item is IBeforeApplyTrunItem<ApplyFinal> c2) OnBefore_ApplyFinal += c2.BeforeApply;
        if (item is IBeforeApplyTrunItem<ApplyRouletNum> c3) OnBefore_ApplyRouletNum += c3.BeforeApply;
        if (item is IBeforeApplyTrunItem<RouletOperator> c4) OnBefore_RouletOperator += c4.BeforeApply;
    }
    public void UnsubEvent(ItemSO item)
    {

        if (item is IAfterApplyTrunIteem<RouletNum, RouletNum> a1) OnAfter_RN_RN -= a1.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, ApplyFinal> a2) OnAfter_RN_AF -= a2.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, ApplyRouletNum> a3) OnAfter_RN_ARN -= a3.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletNum, RouletOperator> a4) OnAfter_RN_RO -= a4.AfterApply;

        if (item is IAfterApplyTrunIteem<RouletOperator, RouletNum> a5) OnAfter_RO_RN -= a5.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, ApplyFinal> a6) OnAfter_RO_AF -= a6.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, ApplyRouletNum> a7) OnAfter_RO_ARN -= a7.AfterApply;
        if (item is IAfterApplyTrunIteem<RouletOperator, RouletOperator> a8) OnAfter_RO_RO -= a8.AfterApply;

        if (item is IBeforeApplyTrunItem<RouletNum> c1) OnBefore_RouletNum -= c1.BeforeApply;
        if (item is IBeforeApplyTrunItem<ApplyFinal> c2) OnBefore_ApplyFinal -= c2.BeforeApply;
        if (item is IBeforeApplyTrunItem<ApplyRouletNum> c3) OnBefore_ApplyRouletNum -= c3.BeforeApply;
        if (item is IBeforeApplyTrunItem<RouletOperator> c4) OnBefore_RouletOperator -= c4.BeforeApply;
    }
    public void OnRouletBefore(Action<RouletData> setter)
    {
        var result = new RouletData(new RouletNum(null), new RouletOperator(null));
        var num = RouletteManager.Instance.RouletData.rouletNum;
        var oper = RouletteManager.Instance.RouletData.rouletOperator;

        OnBefore_RouletNum?.Invoke((RouletNum num) => result.Setter(num));
        OnBefore_ApplyFinal?.Invoke((ApplyFinal num) => result.Setter(num));
        OnBefore_ApplyRouletNum?.Invoke((ApplyRouletNum num) => result.Setter(num));
        OnBefore_RouletOperator?.Invoke((RouletOperator num) => result.Setter(num));
        setter(result);
    }
    public void OnRouletAfter(Action<RouletData> setter)
    {
        var result = new RouletData(new RouletNum(null), new RouletOperator(null));
        var num = RouletteManager.Instance.RouletData.rouletNum;
        var oper = RouletteManager.Instance.RouletData.rouletOperator;
        OnAfter_RN_RN?.Invoke(() => { return num; }, (RouletNum num) => result.Setter(num));
        OnAfter_RN_AF?.Invoke(() => { return num; }, (ApplyFinal num) => result.Setter(num));
        OnAfter_RN_ARN?.Invoke(() => { return num; }, (ApplyRouletNum num) => result.Setter(num));
        OnAfter_RN_RO?.Invoke(() => { return num; }, (RouletOperator num) => result.Setter(num));

        OnAfter_RO_RN?.Invoke(() => { return oper; }, (RouletNum num) => result.Setter(num));
        OnAfter_RO_AF?.Invoke(() => { return oper; }, (ApplyFinal num) => result.Setter(num));
        OnAfter_RO_ARN?.Invoke(() => { return oper; }, (ApplyRouletNum num) => result.Setter(num));
        OnAfter_RO_RO?.Invoke(() => { return oper; }, (RouletOperator num) => result.Setter(num));
        setter(result);
    }
    public void UnSubItems()
    {
        foreach (var item in _currentTrunUseItem)
        {
            UnsubEvent(item);
        }
    }
}
