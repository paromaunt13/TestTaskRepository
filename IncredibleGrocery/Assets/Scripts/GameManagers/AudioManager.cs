using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _soundGroup;
    [SerializeField] private AudioMixerGroup _backgroundMusicGroup;

    private AudioSource _audioSource;

    private const string MusicKey = "MusicEnabled";
    private const string SoundKey = "SoundEnabled";

    private bool _soundEnabled;
    private bool _musicEnabled;

    public bool SoundEnabled => _soundEnabled;
    public bool MusicEnabled => _musicEnabled;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
        
        CheckForKeys();

        PlayBackgroundMusic(_musicEnabled);
    }

    private void PlayBackgroundMusic(bool musicEnabled)
    {
        if (musicEnabled)
        {
            _audioSource.outputAudioMixerGroup = _backgroundMusicGroup;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();         
        }
        else
        {
            _backgroundMusic.Stop();
        }
    }

    private void CheckForKeys()
    {
        if (!PersistentDataManager.CheckKey(MusicKey) || !PersistentDataManager.CheckKey(SoundKey))
        {
            _soundEnabled = true;
            _musicEnabled = true;
        }
        else
        {
            LoadSettings();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (_soundEnabled)
        {           
            _audioSource.outputAudioMixerGroup = _soundGroup;
            _audioSource.PlayOneShot(audioClip);
        }
    }

    public void SwitchSoundState(bool enabled)
    {
        _soundEnabled = enabled;
        SaveSettings();
    }

    public void SwitchMusicState(bool enabled)
    {
        _musicEnabled = enabled;

        PlayBackgroundMusic(_musicEnabled);
        SaveSettings();
    }

    public void SaveSettings()
    {
        PersistentDataManager.SetBool(SoundKey, _soundEnabled);
        PersistentDataManager.SetBool(MusicKey, _musicEnabled);
    }

    public void LoadSettings()
    {
        _soundEnabled = PersistentDataManager.GetBool(SoundKey);
        _musicEnabled = PersistentDataManager.GetBool(MusicKey);
    }
}