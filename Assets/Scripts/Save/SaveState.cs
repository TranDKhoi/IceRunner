using System;

[System.Serializable]
public class SaveState
{

    [NonSerialized] private const int HAT_COUNT = 16;
    public int HighScore { get; set; }
    public int Fish { get; set; }
    public DateTime LastSaveTime { get; set; }
    public int CurrentHatIndex { get; set; }
    private byte[] _UnlockedHatFlag;

    public byte[] UnlockedHatFlag
    {
        get { return _UnlockedHatFlag; }
        set { _UnlockedHatFlag = value; }
    }



    public SaveState()
    {
        HighScore = 0;
        Fish = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
    }
}
