using UnityEngine;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SoundMixer;
    public static AudioManager instance;
    public Sound[] sounds;
    public Sound[] tracks;

    void Start()
    {

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) s.source.Stop();
    }

    public void StopSounds()
    {
        foreach (Sound s in sounds) if (s != null) s.source.Stop();
    }

    public void PlayTrack(string name)
    {
        Sound t = Array.Find(tracks, track => track.name == name);
        if (t != null) t.source.Play();
    }

    public void StopTrack(string name)
    {
        Sound t = Array.Find(tracks, track => track.name == name);
        if (t != null) t.source.Stop();
    }

    public void StopTracks()
    {
        foreach (Sound t in tracks) if (t != null) t.source.Stop();
    }

    public void PauseAll()
    {
        foreach (Sound t in tracks) if (t.source.isPlaying) t.source.Pause(); 
        foreach (Sound s in sounds) if (s.source.isPlaying) s.source.Pause();
    }

    public void ResumeAll()
    {
        foreach (Sound t in tracks) t.source.UnPause();
        foreach (Sound s in sounds) s.source.UnPause();
    }
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = SoundMixer;
        }
        foreach (Sound t in tracks)
        {
            t.source = gameObject.AddComponent<AudioSource>();
            t.source.clip = t.clip;
            t.source.volume = t.volume;
            t.source.pitch = t.pitch;
            t.source.loop = t.loop;
            t.source.outputAudioMixerGroup = MusicMixer;
        }
        MakeSingleton();
    }

    void MakeSingleton()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public void PlayBlast()
    {
        Play("Boom" + Random.Range(1, 9));
    }
}
