using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    BuildableItemSO data { get; }
    public void Configure(BuildableItemSO data);
    public bool TryUpdate();
    public void Update();
    public int GetCurrentUpdate();
    public void Demolished();
}
