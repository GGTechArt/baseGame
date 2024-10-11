using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : ServiceInstallerBase<DataManager>
{
    [SerializeField] WorldDataSO _worldData;
    [SerializeField] LevelDataSO _levelData;
    UserData _data = null;

    string _userDataPath = "/UserData";
    public UserData Data { get => _data; set => _data = value; }
    public LevelDataSO LevelData { get => _levelData; set => _levelData = value; }

    void Start()
    {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _userDataPath = Application.persistentDataPath + _userDataPath;
        Debug.Log(_userDataPath);

        if (Data == null)
        {
            ValidateData();
        }
    }

    //public void LoadData()
    //{
    //    ValidateData();
    //}

    public void ValidateData()
    {
        if (!File.Exists(_userDataPath))
        {
            FileStream _stream = File.Create(_userDataPath);
            _stream.Close();
            Data = new UserData(5, _worldData);
            SaveAllData();
        }

        else
        {
            Data = LoadData<UserData>();
        }
    }

    public T LoadData<T>()
    {
        string _data = File.ReadAllText(_userDataPath);
        return JsonUtility.FromJson<T>(_data);
    }

    public void SaveData<T>(T type)
    {
        string _json = JsonUtility.ToJson(type);
        File.WriteAllText(_userDataPath, _json);
    }

    public void DeleteData()
    {
        if (File.Exists(_userDataPath))
            File.Delete(_userDataPath);
    }

    public void DeleteAllData()
    {
        DeleteData();
        PlayerPrefs.DeleteAll();
        ValidateData();
    }

    public void SaveAllData()
    {
        SaveData(_data);
    }

    protected override DataManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }

    public void SetLevel(LevelDataSO level)
    {
        LevelData = level;
    }
}
