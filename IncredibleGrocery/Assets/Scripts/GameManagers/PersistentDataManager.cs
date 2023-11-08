using UnityEngine;

public class PersistentDataManager
{
    private const string SoundKey = "SoundEnabled";
    private const string MusicKey = "MusicEnabled";
    private const string MoneyKey = "PlayerMoney";
    
    private const string FirstLaunchKey = "FirstLaunch";

    private static bool? _isFirstLaunch;
    private static bool? _musicState;
    private static bool? _soundState;
    
    private static int? _moneyAmount;

    public static bool MusicState
    {
        get
        {
            if (!PlayerPrefs.HasKey(MusicKey)) return true;
            _musicState ??= PlayerPrefs.GetInt((MusicKey), 0) == 1;
            return _musicState.Value;
        }
        set
        {
            _musicState = value;
            PlayerPrefs.SetInt(MusicKey, _musicState.Value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool SoundState
    {
        get
        {
            if (!PlayerPrefs.HasKey(SoundKey)) return true;
            _soundState ??= PlayerPrefs.GetInt((SoundKey), 0) == 1;
            return _soundState.Value;
        }
        set
        {
            _soundState = value;
            PlayerPrefs.SetInt(SoundKey, _soundState.Value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static int MoneyAmount
    {
        get
        {
            if (!PlayerPrefs.HasKey(MoneyKey)) return 0;
            _moneyAmount ??= PlayerPrefs.GetInt(MoneyKey);
            return _moneyAmount.Value;
        }

        set
        {
            _moneyAmount = value;
            PlayerPrefs.SetInt(MoneyKey, _moneyAmount.Value);
            PlayerPrefs.Save();
        }
    }

    public static bool FirstLaunch
    {
        get
        {
            if (!PlayerPrefs.HasKey(FirstLaunchKey)) return true;
            _isFirstLaunch ??= PlayerPrefs.GetInt(FirstLaunchKey, 0) == 1;
            return _isFirstLaunch.Value;
        }
        set
        {
            _isFirstLaunch = value;
            PlayerPrefs.SetInt(FirstLaunchKey, _isFirstLaunch.Value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}