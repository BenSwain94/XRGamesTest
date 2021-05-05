using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] musicClips;
    AudioSource source;

    public bool fadeOut = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
        StartCoroutine("PlayTrack");
    }

    private IEnumerator PlayTrack()
    {
        UpdateTrack();
        source.PlayOneShot(source.clip);

        yield return new WaitForSeconds(source.clip.length);

        StartCoroutine("PlayTrack");
    }
    // Update is called once per frame

    void UpdateTrack()
    {
        int randomTrack = Random.Range(0, musicClips.Length);

        if (source.clip != null)
        {
            while (musicClips[randomTrack] == source.clip)
            {
                randomTrack = Random.Range(0, musicClips.Length);
            }

            source.clip = musicClips[randomTrack];
        }
        else
        {
            source.clip = musicClips[randomTrack];
        }
    }
}
