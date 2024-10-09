using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "New Level Data")]
public class LevelDataSO : ScriptableObject
{
    public string SceneName { get => _sceneName; set => _sceneName = value; }
    [SerializeField] string _sceneName;
    public WaveDataSO Waves { get => _waves; set => _waves = value; }
    [SerializeField] WaveDataSO _waves;
    public List<BuildableItemSO> AvailableItems { get => _availableItems; set => _availableItems = value; }
    [SerializeField] List<BuildableItemSO> _availableItems;
}
