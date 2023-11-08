using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioMixerGroup soundGroup;
    [SerializeField] private AudioMixerGroup backgroundMusicGroup;

    [SerializeField] private SoundsData soundsData;
    
   private Dictionary<SoundType, AudioClip> _soundsDictionary;

    private AudioSource _audioSource;

    private bool _musicEnabled;
    
    public bool SoundEnabled { get; set; }

    public bool MusicEnabled
    {
        get => _musicEnabled;
        set
        {
            if (_musicEnabled == value) return;
            _musicEnabled = value;
            PlayBackgroundMusic(MusicEnabled);
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        
        _audioSource = GetComponent<AudioSource>();
        LoadSettings();
        
        _soundsDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (var soundData in soundsData.SoundsDataList)
        {
            _soundsDictionary[soundData.soundType] = soundData.audioClip;
        }
    }

    private void PlayBackgroundMusic(bool musicEnabled)
    {
        if (musicEnabled)
        {
            _audioSource.outputAudioMixerGroup = backgroundMusicGroup;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
        else
            backgroundMusic.Stop();
    }

    private void LoadSettings()
    {
        SoundEnabled = PersistentDataManager.SoundState;
        MusicEnabled = PersistentDataManager.MusicState;
    }
    
    public void SaveSettings()
    {
        PersistentDataManager.MusicState = MusicEnabled;
        PersistentDataManager.SoundState = SoundEnabled;
    }

    public void PlaySound(SoundType soundType)
    {
        if (!SoundEnabled) return;
        if (!_soundsDictionary.ContainsKey(soundType)) return;
        
        var audioClip = _soundsDictionary[soundType];
        _audioSource.outputAudioMixerGroup = soundGroup;
        _audioSource.PlayOneShot(audioClip);
    }
}