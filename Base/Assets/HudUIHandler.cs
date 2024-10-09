using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUIHandler : MonoBehaviour
{
    GameManager manager;

    [SerializeField] ScrollRect shopScroll;
    [SerializeField] GameObject shopItem;

    void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();

        InstantiateItems();
    }

    public void InstantiateItems()
    {
        for (int i = 0; i < manager.LevelData.AvailableItems.Count; i++)
        {
            int index = i;
            GameObject itemGO = Instantiate(shopItem, shopScroll.content);
            ItemSO item = manager.LevelData.AvailableItems[index];
            itemGO.GetComponent<Button>().onClick.AddListener(() => manager.Build.SelectItem(item));
            itemGO.transform.Find("Icon").GetComponent<Image>().sprite = item.Icon;

        }
    }
}
