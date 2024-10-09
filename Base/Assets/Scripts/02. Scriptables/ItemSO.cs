using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSO : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] Sprite _icon;
    [SerializeField] int _cost;
    [SerializeField] string _itemName;
    [SerializeField] GameObject _prefab;
}
