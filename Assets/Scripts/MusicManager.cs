using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioSource soundEffect;

    [SerializeField]
    AudioClip[] trackList;

    [SerializeField]
    AudioClip[] soundsList;

    public enum Track
    {
        Overworld,
        Battle,
        Title
    }

    public enum Sound
    {
        GuessDebug,
        StudyBreak,
        CheatPopQuiz,
        BonusTeach,
        Struggle,
        Flee
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void onEncounterEnterHandler()
    {
        PlayTrack(Track.Battle);
    }

    public void onEncounterEndHandler()
    {
        PlayTrack(Track.Overworld);
    }

    public void PlayTrack(MusicManager.Track trackID)
    {
        musicSource.clip = trackList[(int)trackID];
        musicSource.Play();
    }

    public void PlaySound(int soundID)
    {
        soundEffect.clip = soundsList[(int)soundID];
        soundEffect.Play();
    }
}
