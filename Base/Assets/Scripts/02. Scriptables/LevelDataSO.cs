using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "New Level Data")]
public class LevelDataSO : ScriptableObject
{
    public string LevelID { get => _levelID; set => _levelID = value; }
    [SerializeField] string _levelID;
    public string SceneName { get => _sceneName; set => _sceneName = value; }
    [SerializeField] string _sceneName;
    public WaveDataSO Waves { get => _waves; set => _waves = value; }
    [SerializeField] WaveDataSO _waves;
    public List<int> StarsValues { get => _starsValues; set => _starsValues = value; }
    [SerializeField] List<int> _starsValues;
    public List<BuildableItemSO> AvailableItems { get => _availableItems; set => _availableItems = value; }
    [SerializeField] List<BuildableItemSO> _availableItems;    
}
