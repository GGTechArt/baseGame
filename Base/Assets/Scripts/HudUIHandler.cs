using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUIHandler : MonoBehaviour
{
    GameManager manager;

    [SerializeField] ScrollRect shopScroll;
    [SerializeField] GameObject shopItem;

    [SerializeField] TextMeshProUGUI waveCounterText;
    [SerializeField] TextMeshProUGUI scoreCounterText;

    void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();

        manager.Waves.WavesStarted += UpdateWaveCounter;
        manager.Score.ScoreChangedStarted += UpdateScoreCounter;

        InstantiateItems();
    }

    public void InstantiateItems()
    {
        if (manager.LevelData.AvailableItems.Count > 0)
        {
            for (int i = 0; i < manager.LevelData.AvailableItems.Count; i++)
            {
                int index = i;
                GameObject itemGO = Instantiate(shopItem, shopScroll.content);
                BuildableItemSO item = manager.LevelData.AvailableItems[index];
                itemGO.GetComponent<Button>().onClick.AddListener(() => manager.Build.SelectItem(item));
                itemGO.transform.Find("Icon").GetComponent<Image>().sprite = item.Icon;
            }

            manager.Build.SelectItem(manager.LevelData.AvailableItems[0]);
        }
    }

    public void UpdateWaveCounter(int wave)
    {
        waveCounterText.text = wave.ToString() + "/" + manager.LevelData.Waves.WaveDataList.Count;
    }

    public void UpdateScoreCounter(int score)
    {
        scoreCounterText.text = score.ToString();
    }
}
