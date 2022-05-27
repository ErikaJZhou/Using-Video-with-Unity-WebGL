using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/* References: */
/*
    Most of this code was taken from this Simmer tutorial which got it from the YouTube tutorial by irls.
    The Stack Overflow solution from Jz Konain to their own question gave a key line of code for using a video file in Assets with Unity WebGL.

    "Adding Video to Unity WebGL" | SIMMER.io
        https://simmer.io/articles/adding-video-to-unity-webgl

    Stack Overflow:
        "Webgl unity build won't play Video" asked by Jz Konain
        https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video

    "Play Video in Unity WebGL" by irls on YouTube (Mar 25, 2021).
        https://www.youtube.com/watch?v=VKRS9HuIsUY
*/

public class StreamingAssetsVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"cat-attacking-croc-shoe.mp4"); // from Jz Konain's Stack Overflow answer
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
        videoPlayer.Play();
        videoPlayer.isLooping = false;
    }
}
