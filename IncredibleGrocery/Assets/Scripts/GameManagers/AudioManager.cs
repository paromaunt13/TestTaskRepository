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
    
    public bool SoundEnabled { get; private set; }
    public bool MusicEnabled { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        
        Instance = this;

        //DontDestroyOnLoad((this.gameObject));
        _audioSource = GetComponent<AudioSource>();

        LoadSettings();

        PlayBackgroundMusic(MusicEnabled);
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

    public void SwitchSoundState(bool enabled)
    {
        SoundEnabled = enabled;
    }

    public void SwitchMusicState(bool enabled)
    {
        MusicEnabled = enabled;

        PlayBackgroundMusic(MusicEnabled);
    }
}