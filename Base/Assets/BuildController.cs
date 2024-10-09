using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] ItemSO itemSelected;

    public void SelectItem(ItemSO newItem)
    {
        itemSelected = newItem;
    }
}
