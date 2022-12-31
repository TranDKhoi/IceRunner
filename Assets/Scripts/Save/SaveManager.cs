using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _ins;
    public static SaveManager ins
    {
        get { return _ins; }
    }


    //fields
    public SaveState save;
    private const string saveFileName = "data.icerunner";
    private BinaryFormatter formatter;


    //action
    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;

    private void Awake()
    {
        _ins = this;
        formatter = new BinaryFormatter();
        //try and load previous save state
        Load();
    }

    public void Load()
    {
        try
        {
            var file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.Open, FileAccess.Read);
            save = formatter.Deserialize(file) as SaveState;
            file.Close();
            OnLoad?.Invoke(save);
        }
        catch
        {
            Save();
        }
    }

    public void Save()
    {
        if (save is null)
            save = new SaveState();

        save.LastSaveTime = DateTime.Now;

        var file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, save);
        file.Close();
        OnSave?.Invoke(save);
    }
}
