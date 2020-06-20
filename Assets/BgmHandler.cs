using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>This is used within Global to manage the background music tracks (stems) within a script. Please use OGG Vorbis for clips!
/// All tracks must be the same length for proper looping as of now.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/> 
/// </summary>
public class BgmHandler : MonoBehaviour
{
    public AudioClip[] tracks;
    public int[] pauseDisablers;
    public float trimTreshhold = 0.5f;
    public bool loop = true;
    public bool playOnAwake = false;
    public AudioSource[] trackSources;
    public AudioSource[] trackSources2;
    private Coroutine[] fading;
    public void Awake()
    {
        for(int i=0;i < tracks.Length;i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            if(tracks[i].name.Contains("mp3")) source.clip = Common.TrimSilence(tracks[i],trimTreshhold);
            else source.clip = tracks[i];
            source.loop = loop;
            source.playOnAwake = false;
        }
        fading = new Coroutine[tracks.Length];
    }
    public void Start()
    {
        foreach(AudioSource source in gameObject.GetComponents<AudioSource>())
        {
            Debug.Log(source);
            trackSources2 = trackSources;
            trackSources = new AudioSource[trackSources.Length+1];
            for(int i=0;i < trackSources.Length;i++)
            {
                if(trackSources2.Length > i)
                {
                    if(trackSources2[i] != null)
                        trackSources.SetValue(trackSources2[i],i);
                }
                else break;
            }
            trackSources.SetValue(source,trackSources.Length-1);
        }
        if(playOnAwake) Play();
    }
    public void Play()
    {
        Play(new int[0]);
    }
    public void Play(int[] disablers)
    {
        for(int i=0;i < trackSources.Length;i++)
        {
            if(disablers.Length > 0)
            {
                bool disabled = false;
                for(int e=0;e < disablers.Length;e++)
                {
                    if(disablers[e] == i)
                    {
                        disabled = true;
                        break;
                    }
                }
                if(!disabled) trackSources[i].Play();
            }
            else trackSources[i].Play();
        }
    }
    public void FadePauseDisablers(bool toggleOut = true)
    {
        for(int i=0;i < pauseDisablers.Length;i++)
        {
            if(toggleOut)
            {
                if(fading[i] != null) StopCoroutine(fading[i]);
                fading[i] = StartCoroutine(FadeOut(trackSources[pauseDisablers[i]]));
            }
            else
            {
                if(fading != null) StopCoroutine(fading[i]);
                fading[i] = StartCoroutine(FadeIn(trackSources[pauseDisablers[i]]));
            }
        }
    }
    public void Stop()
    {
        for(int i=0;i < trackSources.Length;i++)
        {
            trackSources[i].Stop();
        }
    }
    public void Pause()
    {
        for(int i=0;i < trackSources.Length;i++)
        {
            trackSources[i].Pause();
        }
    }
    public void UnPause()
    {
        for(int i=0;i < trackSources.Length;i++)
        {
            trackSources[i].UnPause();
        }
    }
    private IEnumerator FadeOut(AudioSource source)
    {
        while(source.volume > 0)
        {
            source.volume -= 0.1f;
            yield return null;
        }
        source.volume = 0;
        yield return null;
    }
    private IEnumerator FadeIn(AudioSource source)
    {
        while(source.volume < 1)
        {
            source.volume += 0.1f;
            yield return null;
        }
        source.volume = 1;
        yield return null;
    }
}
