using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : ServiceInstallerBase<DataManager>
{
    UserData _data;

    string _userDataPath = "UserData";

    void Start()
    {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _userDataPath = Application.persistentDataPath + _userDataPath;

        if (_data == null)
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        ValidateData();
    }

    public void ValidateData()
    {
        if (!File.Exists(_userDataPath))
        {
            FileStream _stream = File.Create(_userDataPath);
            _stream.Close();

            _data = new UserData(0);
        }
        else
        {
            _data = LoadData<UserData>();
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
        Debug.Log("Se han guardado los ajustes del juego");
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
        LoadData();
    }

    protected override DataManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }
}
