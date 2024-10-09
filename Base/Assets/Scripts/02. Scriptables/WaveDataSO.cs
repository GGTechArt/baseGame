using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves Data", menuName = "New Waves Data")]
public class WaveDataSO : ScriptableObject
{
  [SerializeField]  List<WaveData> _waveDataList;

    public List<WaveData> WaveDataList { get => _waveDataList; set => _waveDataList = value; }
}
