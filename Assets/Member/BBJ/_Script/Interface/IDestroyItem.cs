using BBJ;
using System;
using UnityEngine;

public interface IDestroyItem: IItem
{
    public Action<IDestroyItem> Destroyed { get; set; }
}
