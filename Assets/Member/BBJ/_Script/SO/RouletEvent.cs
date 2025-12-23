using Core;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RouletEvent", menuName = "RouletSystem/Event")]
public class RouletEvent : EventChannel<Action<RouletData>>
{
    
}
