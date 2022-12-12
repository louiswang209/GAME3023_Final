using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    [SerializeField]
    AudioClip[] footstepSounds;

    [SerializeField]
    AudioSource footstepSource;
    public void PlayFootsteps()
    {
        footstepSource.clip = footstepSounds[0];
        footstepSource.Play();
    }
}
