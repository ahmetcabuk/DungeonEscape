using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

[System.Serializable]
public class AudioClipPair
{
    public string key;
    public AudioClip value;
}

[CreateAssetMenu(fileName = "New Sound", menuName = "New Sound")]
public class ScriptableSounds : ScriptableObject
{
    public List<AudioClipPair> AudioClips = new List<AudioClipPair>();

    public AudioClip GetAudioClip(string audioKey)
    {
        try
        {
            return AudioClips.Find(i => i.key.Equals(audioKey)).value;
        }
        catch (Exception e)
        {
            Debug.Log("Exception occurred while trying to get AudioClip from scriptable object with the message " + e.Message);
            return null;
        }
    }
}
