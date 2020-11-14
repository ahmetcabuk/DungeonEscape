using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudios : MonoBehaviour
{
    public List<string> backgroundAudios = new List<string>();
    

    void Start()
    {
        for (int i = 0; i < backgroundAudios.Count; i++)
        {
            AudioManager.Instance?.PlayBackgroundMusic(backgroundAudios[i]);
        }
    }

}
