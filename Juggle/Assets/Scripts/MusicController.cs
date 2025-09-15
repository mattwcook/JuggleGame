using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] songList;
    AudioClip currentTrack;
    float timer = 0;
    float songLength;
    AudioSource audioSource;
    int trackNumber;

    // Start is called before the first frame update
    void Start()
    {
        trackNumber = Random.Range(0, songList.Length);
        currentTrack = songList[trackNumber];
        songLength = currentTrack.length;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(currentTrack);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > songLength)
        {
            trackNumber = (trackNumber + 1) % songList.Length;
            currentTrack = songList[trackNumber];
            audioSource.PlayOneShot(currentTrack);
            songLength = currentTrack.length;
        }
    }
}
