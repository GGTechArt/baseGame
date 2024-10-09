using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BuildableItemSO : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] Sprite _icon;
    [SerializeField] int _cost;
    [SerializeField] string _itemName;
    [SerializeField] GameObject _prefab;

    [SerializeField] UpdatesSO _updates;

    public Sprite Icon { get => _icon; set => _icon = value; }
    public int Cost { get => _cost; set => _cost = value; }
    public string ItemName { get => _itemName; set => _itemName = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public UpdatesSO Updates { get => _updates; set => _updates = value; }
}
