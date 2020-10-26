using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public enum AudioMixerGroupEnum
{
    MASTER_GROUP,
    MUSIC_GROUP,
    SFX_GROUP
}

public class AudioManager : Singleton<AudioManager>
{
    // To check what audio is playing for debugging purposes. (Might be extended to take action when a specific sound is playing.)
    private List<string> nowPlayingList = new List<string>();

    // To check what audio clip is playing as background music for debugging purposes.
    private AudioClip nowPlayingBackgroundClip;

    // ScriptableObject that holds AudioClips that will be used.
    [SerializeField] private ScriptableSounds audioClipsSO = null;

    // AudioMixer and AudioMixerGroup variables to hold for further processes.
    public AudioMixer mainMixer;
    public AudioMixerGroup masterGroup, musicGroup, sfxGroup;

    // To control exposed volume's names of Audio Mixer.
    [SerializeField] private string masterGroupExposedVolumeName = "MasterVolume";
    [SerializeField] private string musicGroupExposedVolumeName = "MusicVolume";
    [SerializeField] private string sfxGroupExposedVolumeName = "SFXVolume";

    // To save volumes before muting audio.
    private float volumeBeforeMuteMaster;
    private float volumeBeforeMuteMusic;
    private float volumeBeforeMuteSFX;

    // GameObject to be instantiated on-the-fly to hold AudioSource components.
    public GameObject audioSourceHolderGO;

    // To hold audio source of background music for later use.
    private AudioSource backgroundMusicAudioSource;
    
    private void Awake()
    {
        mainMixer.GetFloat(masterGroupExposedVolumeName, out volumeBeforeMuteMaster);
        mainMixer.GetFloat(musicGroupExposedVolumeName, out volumeBeforeMuteMusic);
        mainMixer.GetFloat(sfxGroupExposedVolumeName, out volumeBeforeMuteSFX);
    }

    
    private void Start()
    {
        DontDestroyOnLoad (gameObject);
    }

    #region Play Sound Logic
    #region SFX3D
    /// <summary>
    /// Plays Audio in 3D on SFX AudioMixerGroup.
    /// </summary>
    /// <param name="mixerGroup">Mixer Group to put clip on to.</param>
    /// <param name="clip">AudioClip to play.</param>
    /// <param name="soundPosition">SoundPosition on world space.</param>
    /// <param name="onClipStart">Actions to be taken on clip start.</param>
    /// <param name="onClipEnd">Actions to be taken on clip end.</param>
    public void PlaySFXAudio3D(AudioClip clip, Vector3 soundPosition, Action onClipStart = null, Action onClipEnd = null)
    {

        AudioSource source = PoolingSystem.Instance.GetAudioSource();
        source.transform.position = soundPosition;
        source = SetAudioSourceSettings(source, AudioMixerGroupEnum.SFX_GROUP, clip, true, false);      // To set audio source settings like mixer groups.

        OnClipStart(clip.name, onClipStart);                                                            // Call the preparations for the clip's start.
        source.Play();                                                                                  // Play audio.
        _= OnClipEnd(clip.name, (int)(clip.length * 1000), onClipEnd);                                  // Call the preparations for the clip's ending.
    }

    // String version of the original
    public void PlaySFXAudio3D(string clip, Vector3 soundPosition, Action onClipStart = null, Action onClipEnd = null)  
    {
        PlaySFXAudio3D(audioClipsSO.GetAudioClip(clip), soundPosition, onClipStart, onClipEnd);
    }
    #endregion

    #region SFX2D
    /// <summary>
    /// Plays Audio in 2D on SFX AudioMixerGroup.
    /// </summary>
    /// <param name="mixerGroup">Mixer Group to put clip on to.</param>
    /// <param name="clip">AudioClip to play.</param>
    /// <param name="onClipStart">Actions to be taken on clip start.</param>
    /// <param name="onClipEnd">Actions to be taken on clip end.</param>
    public void PlaySFXAudio2D(AudioClip clip, Action onClipStart = null, Action onClipEnd = null)
    {
        // TODO: Camera positions need to be get from a CameraController which holds the current camera viewing. For better performance.


        AudioSource source = PoolingSystem.Instance.GetAudioSource();
        source = SetAudioSourceSettings(source, AudioMixerGroupEnum.SFX_GROUP, clip, false, false);     // To set audio source settings like mixer groups.

        OnClipStart(clip.name, onClipStart);                                                            // Call the preparations for the clip's start.
        source.Play();                                                                                  // Play audio.
        _= OnClipEnd(clip.name, (int)(clip.length * 1000), onClipEnd);                                  // Call the preparations for the clip's ending.
    }

    // String version of the original
    public void PlaySFXAudio2D(string clip, Action onClipStart = null, Action onClipEnd = null)     
    {
        PlaySFXAudio2D(audioClipsSO.GetAudioClip(clip), onClipStart, onClipEnd);
    }
    #endregion

    #region BackgroundMusic
    /// <summary>
    /// Plays an AudioClip as background music.
    /// </summary>
    /// <param name="clip">AudioClip to play as background music.</param>
    public void PlayBackgroundMusic(AudioClip clip)
    {
        AudioSource source;
        if (backgroundMusicAudioSource == null)
        {
            source = PoolingSystem.Instance.GetAudioSource();
            backgroundMusicAudioSource = source;
        }
        else
        {
            source = backgroundMusicAudioSource;
        }

        source = SetAudioSourceSettings(source, AudioMixerGroupEnum.MUSIC_GROUP, clip, false, true);        // To set audio source settings like mixer groups.
        source.Play();                                                                                      // Play audio.
    }

    // String version of the original
    public void PlayBackgroundMusic(string clip)   
    {
        PlayBackgroundMusic(audioClipsSO.GetAudioClip(clip));
    }
    #endregion

    #endregion

    #region AudioSource Settings
    /// <summary>
    /// Sets AudioSource settings like MixerGroup and AudioClip settings.
    /// </summary>
    /// <param name="audioSource">AudioSource to set.</param>
    /// <param name="mixerGroup">Mixer Group to put clip on to.</param>
    /// <param name="clip">AudioClip to play.</param>
    /// <param name="isSpatial">Is the sound 3D?</param>
    /// <param name="isLoop">Is the clip will be looping?</param>
    private AudioSource SetAudioSourceSettings(AudioSource audioSource, AudioMixerGroupEnum mixerGroup, AudioClip clip, bool isSpatial, bool isLoop)
    {
        audioSource.clip = clip;

        if(isSpatial)
        {
            audioSource.spatialize = true;
            audioSource.spatialBlend = 1.0f;
        }
        else
        {
            audioSource.spatialize = false;
            audioSource.spatialBlend = 0.0f;
        }

        audioSource.loop = isLoop;

        switch (mixerGroup)
        {
            case AudioMixerGroupEnum.MASTER_GROUP:
                audioSource.outputAudioMixerGroup = masterGroup;
                break;
            case AudioMixerGroupEnum.MUSIC_GROUP:
                audioSource.outputAudioMixerGroup = musicGroup;
                break;
            case AudioMixerGroupEnum.SFX_GROUP:
                audioSource.outputAudioMixerGroup = sfxGroup;
                break;
            default:
                Debug.Log("Wrong type of AudioMixerGroupEnum is selected for setting AudioSourceSettings on " + mixerGroup.ToString() + ". Please fix it.");
                break;
        }

        return audioSource;
    }
    #endregion

    #region Volume Adjustments
    /// <summary>
    /// Sets volume of the specified MixerGroup.
    /// </summary>
    /// <param name="mixerGroup">MixerGroup to set volume.</param>
    /// <param name="volume">Volume of the specified MixerGroup.</param>
    public void SetVolumeMixerGroup(AudioMixerGroupEnum mixerGroup, float volume)
    {
        switch (mixerGroup)
        {
            case AudioMixerGroupEnum.MASTER_GROUP:
                mainMixer.SetFloat(masterGroupExposedVolumeName, volume);
                break;
            case AudioMixerGroupEnum.MUSIC_GROUP:
                mainMixer.SetFloat(musicGroupExposedVolumeName, volume);
                break;
            case AudioMixerGroupEnum.SFX_GROUP:
                mainMixer.SetFloat(sfxGroupExposedVolumeName, volume);
                break;
            default:
                Debug.LogError("Wrong type of AudioMixerGroupEnum is selected for setting mixer group " + mixerGroup.ToString() + " volume. Please fix it.");
                break;
        }
    }
    /// <summary>
    /// Mutes the specified MixerGroup.
    /// </summary>
    /// <param name="mixerGroup">MixerGroup to mute.</param>
    public void MuteMixerGroup(AudioMixerGroupEnum mixerGroup)
    {
        switch (mixerGroup)
        {
            case AudioMixerGroupEnum.MASTER_GROUP:
                mainMixer.GetFloat(masterGroupExposedVolumeName, out volumeBeforeMuteMaster);
                mainMixer.SetFloat(masterGroupExposedVolumeName, -80f);
                break;
            case AudioMixerGroupEnum.MUSIC_GROUP:
                mainMixer.GetFloat(musicGroupExposedVolumeName, out volumeBeforeMuteMusic);
                mainMixer.SetFloat(musicGroupExposedVolumeName, -80f);
                break;
            case AudioMixerGroupEnum.SFX_GROUP:
                mainMixer.GetFloat(sfxGroupExposedVolumeName, out volumeBeforeMuteSFX);
                mainMixer.SetFloat(sfxGroupExposedVolumeName, -80f);
                break;
            default:
                Debug.LogError("Wrong type of AudioMixerGroupEnum is selected for setting mixer group " + mixerGroup.ToString() + " volume. Please fix it.");
                break;
        }
    }
    /// <summary>
    /// Mutes the specified MixerGroup.
    /// </summary>
    /// <param name="mixerGroup">MixerGroup to mute.</param>
    public void UnMuteMixerGroup(AudioMixerGroupEnum mixerGroup)
    {
        switch (mixerGroup)
        {
            case AudioMixerGroupEnum.MASTER_GROUP:
                mainMixer.SetFloat(masterGroupExposedVolumeName, volumeBeforeMuteMaster);
                break;
            case AudioMixerGroupEnum.MUSIC_GROUP:
                mainMixer.SetFloat(musicGroupExposedVolumeName, volumeBeforeMuteMusic);
                break;
            case AudioMixerGroupEnum.SFX_GROUP:
                mainMixer.SetFloat(sfxGroupExposedVolumeName, volumeBeforeMuteSFX);
                break;
            default:
                Debug.LogError("Wrong type of AudioMixerGroupEnum is selected for setting mixer group " + mixerGroup.ToString() + " volume. Please fix it.");
                break;
        }
    }
    #endregion

    #region Event Trackers
    /// <summary>
    /// Things to do on clip start.
    /// </summary>
    /// <param name="clipName">ClipName to add to NowPlayingList.</param>
    /// <param name="onClipStart">Actions to be taken on clip start.</param>
    private void OnClipStart(string clipName, Action onClipStart = null)
    {
        onClipStart?.Invoke();
        nowPlayingList.Add(clipName);
    }

    /// <summary>
    /// Things to do on clip end.
    /// </summary>
    /// <param name="clipName">ClipName to add to NowPlayingList.</param>
    /// <param name="clipDuration">ClipDuration to wait for (needs to be in milliseconds and an integer number).</param>
    /// <param name="onClipEnd">Actions to be taken on clip end.</param>
    /// <returns></returns>
    private async Task OnClipEnd(string clipName, int clipDuration, Action onClipEnd = null)
    {
        await Task.Delay(clipDuration);
        nowPlayingList.Remove(clipName);

        //if (toBeDestroyed != null)
        //    Destroy(toBeDestroyed);

        onClipEnd?.Invoke();
    }
    #endregion
}
