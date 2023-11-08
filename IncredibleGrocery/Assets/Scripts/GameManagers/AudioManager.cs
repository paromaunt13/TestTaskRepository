using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;


public class AudioManager : MonoBehaviour
{
    
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioMixerGroup soundGroup;
    [SerializeField] private AudioMixerGroup backgroundMusicGroup;

    private AudioSource _audioSource;

    private bool _musicEnabled;
    
    public bool SoundEnabled { get; set; }

    public bool MusicEnabled
    {
        get => _musicEnabled;
        set
        {
            if (_musicEnabled != value)
            {
                _musicEnabled = value;
                PlayBackgroundMusic(MusicEnabled);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        LoadSettings();
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

    public void PlaySound(AudioClip audioClip)
    {
        if (!SoundEnabled) return;
        _audioSource.outputAudioMixerGroup = soundGroup;
        _audioSource.PlayOneShot(audioClip);
    }
}