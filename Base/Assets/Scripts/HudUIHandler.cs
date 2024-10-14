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
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] Button pauseButton;

    [SerializeField] Button demolitionButton;
    [SerializeField] Button speedButton;
    [SerializeField] Sprite demolitionEnableSprite, demolitionDisableSprite;

    void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();

        manager.Waves.WavesStarted += UpdateWaveCounter;
        manager.Score.ScoreChangedStarted += UpdateScoreCounter;
        manager.Build.DemolitionStateChanged += UpdateDemolitionSprite;
        manager.timeChanged += UpdateSpeedButton;
        manager.GameStateChanged += GameStateChanged;

        pauseButton.onClick.AddListener(() => manager.PauseGame());
        demolitionButton.onClick.AddListener(() => manager.Build.ChangeDemolitionMode(manager.Build.GetDemolitionState() ? false : true));
        speedButton.onClick.AddListener(() => manager.NextTimeScale());

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
                itemGO.transform.Find("ShopUIItem/Icon").GetComponent<Image>().sprite = item.Icon;
                itemGO.transform.Find("ShopUIItem/PriceGroup/PriceText").GetComponent<TextMeshProUGUI>().text = item.Cost.ToString();
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

    public void UpdateDemolitionSprite(bool activated)
    {
        demolitionButton.image.sprite = activated ? demolitionEnableSprite : demolitionDisableSprite;
    }
    public void UpdateSpeedButton(float newSpeed)
    {
        speedText.text = newSpeed.ToString();
    }

    public void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.PREPARE:
                speedButton.interactable = true;
                demolitionButton.interactable = true;
                break;
            case GameState.PLAYING:
                speedButton.interactable = true;
                demolitionButton.interactable = true;
                break;
            case GameState.PAUSED:
                speedButton.interactable = false;
                demolitionButton.interactable = false;
                break;
            case GameState.FINISHED:
                speedButton.interactable = false;
                demolitionButton.interactable = false;
                break;
        }
    }
}
