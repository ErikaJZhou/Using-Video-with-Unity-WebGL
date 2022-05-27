using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/* References: */
/*
    This code was taken from this Simmer tutorial which got it from the YouTube tutorial by irls.

    "Adding Video to Unity WebGL" | SIMMER.io
    https://simmer.io/articles/adding-video-to-unity-webgl

    "Play Video in Unity WebGL" by irls on YouTube (Mar 25, 2021).
    https://www.youtube.com/watch?v=VKRS9HuIsUY
*/

public class Video_Controller : MonoBehaviour
{
    public string url;
    public VideoPlayer vidplayer;

    // Start is called before the first frame update
    void Start()
    {
        vidplayer = GetComponent<VideoPlayer>();
        vidplayer.url = url;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Play();
        }

    }

    void Play()
    {
        vidplayer.Play();
        vidplayer.isLooping = false;
    }
}