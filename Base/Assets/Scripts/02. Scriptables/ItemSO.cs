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

    public Sprite Icon { get => _icon; set => _icon = value; }
    public int Cost { get => _cost; set => _cost = value; }
    public string ItemName { get => _itemName; set => _itemName = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
