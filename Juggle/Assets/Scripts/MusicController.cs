using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] songList;
    [SerializeField] AudioClip gameOverMusic;
    AudioClip currentTrack;
    float timer = 0;
    float songLength;
    AudioSource audioSource;
    int trackNumber;
    bool gameOver = false;

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
        if (timer > songLength && gameOver == false)
        {
            audioSource.Stop();
            timer = 0;
            trackNumber = (trackNumber + 1) % songList.Length;
            //currentTrack = songList[trackNumber];
            //audioSource.PlayOneShot(currentTrack);
            audioSource.clip = songList[trackNumber];
            audioSource.Play();
            songLength = currentTrack.length;
        }
    }

    public void GameOver()
    {
        audioSource.Stop();
        audioSource.clip = gameOverMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
