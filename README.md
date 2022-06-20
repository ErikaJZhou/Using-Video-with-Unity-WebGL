# Using Video with Unity WebGL

Test project and tutorial for how to include a video in a Unity WebGL project.

Requested by Dr. Fang Wang from University of Missouri to help with part of a Summer 2022 study abroad trip project.


## Motivation
We wanted a way to add video to a Unity WebGL project from a video hosted in the cloud/accessed via a URL.

Ideally it would be easy, quick, and simple.

A solution that is free and using tools that IT students are already familiar with is a plus.


## Try out the test project!
**Tested with:** Unity editor version 2020.3.30f1

Using `StreamingAssets`

1. Clone this repository to your local machine.
1. Open the Unity project in your Unity editor.
1. Open the scene called `StreamingAssets Video`.
    - There is another scene in the project called `GitHub Pages Video` that uses a different means of playing the video. You can check that one out too, if you'd like. It uses a URL to a video file as the Source of the Video Player component. That scene was made following the referenced tutorials from Simmer and irls.
1. Press Play to test the project in Play Mode. If you click in Game view or press any key, a brief cat video should play.
1. Go to Build Settings (File>Build Settings) and Add Open Scenes.
1. Select WebGL as the platform.

    ![Build Settings for Unity WebGL](/Screenshots/unity-webgl-build-settings-1.png "Build Settings")
    - If the WebGL Build Support module is not installed, install it through Unity Hub.
    ![Add modules from the Installs page of Unity Hub](/Screenshots/unity-hub-installs-add-modules.png "Add modules for Unity install")

1. "Build And Run" the Unity project. You may need to wait several minutes for the build to complete. [(Simmer.io)][Simmer.io Export WebGL Upload to Web]
    - Once the build is finished, the WebGL project opens in your browser on localhost. If you click in the Unity canvas or press any key, the cat video should play.
    ![Unity WebGL project in browser on localhost after a Build And Run](/Screenshots/unity-webgl-streamingassets-video-test.png "Unity WebGL project in browser")


## Tutorial
**Tested with:** Unity editor version 2020.3.30f1 | **Difficulty:** Beginner | **Complexity:** Simple

(This tutorial was adapted from tutorials by [Simmer][SIMMER Adding Video] and [irls][irls Play Video Unity WebGL]. The StreamingAssets trick for playing a video from Assets in a Unity WebGL project was from Jz Konain's [Stack Overflow][Stack Overflow WebGL Video] solution.)

1. Start a Unity 3D project.
1. Create a new folder in Assets called `StreamingAssets` and put your video file in it.
1. Add a Raw Image to your scene. Right click in Hierarchy, UI>Raw Image.
1. In Assets, create a new Render Texture. Right click in Assets, Create>Render Texture.
1. Add a Video Player component to the Raw Image.
1. In the Video Player component, uncheck Play on Awake.
1. Drag and drop the Render Texture into the Texture field of the Raw Image component and into the Target Texture field of the Video Player component.
    
    ![Raw Image and Video Player components in Inspector window](/Screenshots/raw-image-and-video-player-in-inspector.png "Raw Image and Video Player components in Inspector")
1. Create a new C# script for the video controller. I named mine "StreamingAssetsVideoController". Open it up in your text editor.
1. Copy and paste the following code into your C# script.
    ```
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
            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"your-video-file.mp4"); // from Jz Konain's Stack Overflow answer
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
    ```
1. Replace "your-video-file.mp4" with the name of your video file. Save the script.
1. Add the video controller script as a component to the Raw Image.
1. Press Play to test the project in Play Mode. If you click in Game view or press any key, your video should play.
1. On the Raw Image, you can change Scale in Rect Transform, change Anchor Presets, etc. to adjust the size of the video to how you'd like. You can also change the Aspect Ratio in the Video Player component.
1. Once you're happy with it and have tested that the project works in Play mode, you're ready to build your project for WebGL. Go to Build Settings (File>Build Settings) and Add Open Scenes.
1. Select WebGL as the platform.
    ![Build Settings for Unity WebGL](/Screenshots/unity-webgl-build-settings-1.png "Build Settings")
    - If the WebGL Build Support module is not installed, install it through Unity Hub.
    ![Add modules from the Installs page of Unity Hub](/Screenshots/unity-hub-installs-add-modules.png "Add modules for Unity install")
1. "Build And Run" the Unity project. You may need to wait several minutes for the build to complete. [(Simmer.io)][Simmer.io Export WebGL Upload to Web]
    - Once the build is finished, the WebGL project opens in your browser on localhost. If you click in the Unity canvas or press any key, your video should play.
    ![Unity WebGL project in browser on localhost after a Build And Run](/Screenshots/unity-webgl-streamingassets-video-test.png "Unity WebGL project in browser")


## Problems and Potential Solutions (Process Notes)
- Issue that using video file asset in Unity project with Unity Video Player directly for a WebGL build would not work. [(Unity Forum)][Unity Forum Video Player WebGL]
    - **There is a way around this!** In Assets, create a folder named `StreamingAssets` and put the video file in it. Use this code in the video control script and replace the video filename with that of the desired video file. `videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"your-video-file.mp4"); ` [(Stack Overflow)][Stack Overflow WebGL Video]
- From what we found, YouTube video embeds currently do not seem to work with Unity WebGL. [(Unity Answers)][Unity Answers embed YouTube], [(Quora)][Quora play Youtube]
- GitHub has a file size push limit of 100 MB. [(GitHub Docs)][GitHub Docs Large Files]
- GitHub Pages can be used to host a small video which can be used with Unity WebGL. [(SIMMER)][SIMMER Adding Video]
- [Git Large File Storage][Git LFS] can be used to track larger files. It is not permitted with GitHub Pages. [(GitHub Docs)][GitHub Docs Git LFS]
- YouTube, Google Drive, OneDrive, Streamable, etc. (at least by default) have the video on a webpage rather than as a direct link to the file itself.
- An Amazon S3 Bucket could be used to get around the storage limit issue.
    - However, this may be somewhat involved to configure and more complicated than desired with students needing an AWS account, and billing may add extra complexity.
    - Security, but allowing public access over the web
- Dropbox offers 2 GB of free storage. A direct link to a video file can be obtained. [(Dropbox Chooser)][Dropbox Chooser]
    - The Dropbox Chooser tool can be used to get a temporary direct link that works, though it expires after 4 hours.
    - A link to the raw file can be obtained by adding query parameter `raw=1` to the shared link, but this causes an HTTP redirect. [(Dropbox force render)][Dropbox force render]
        - Unity VideoPlayer cannot read the file from the link to the raw file or the redirected URL.
- A direct download link to a file on OneDrive can be obtained. [(Bydik)][Bydik]
    - University of Missouri student OneDrive accounts do not appear to allow the Embed feature (possibly for security reasons), so this would not work with the student OneDrive accounts.
    - Also, had an issue with the direct link for a video on my personal OneDrive account not working. Might have been a server issue.
    - OneDrive might still add a small wrapper to the video file so it might not work


## References:
1. Bydik - "How to Get Direct or Permanent Link for OneDrive Files?" by Bydik Team
[https://bydik.com/onedrive-direct-link/](https://bydik.com/onedrive-direct-link/)

    [Bydik]: <https://bydik.com/onedrive-direct-link/>

1. Dropbox for Developers - Chooser
[https://www.dropbox.com/developers/chooser](https://www.dropbox.com/developers/chooser)

    [Dropbox Chooser]: <https://www.dropbox.com/developers/chooser>

1. Dropbox Help:

    "How to force a shared link to download or render"
    [https://help.dropbox.com/files-folders/share/force-download](https://help.dropbox.com/files-folders/share/force-download "How to force a shared link to download or render")

    [Dropbox force render]: <https://help.dropbox.com/files-folders/share/force-download>
    "How to force a shared link to download or render"

1. GitHub Docs:

    "About Git Large File Storage"
    [https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-git-large-file-storage](https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-git-large-file-storage "About Git Large File Storage")

    [GitHub Docs Git LFS]: <https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-git-large-file-storage>
    "About Git Large File Storage"

    "About large files on GitHub"
    [https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-large-files-on-github](https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-large-files-on-github "About large files on GitHub")

    [GitHub Docs Large Files]: <https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-large-files-on-github> "About large files on GitHub"

    "About storage and bandwidth usage"
    [https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-storage-and-bandwidth-usage](https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-storage-and-bandwidth-usage "About storage and bandwidth usage")

    [GitHub Docs Git LFS storage]: <https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-storage-and-bandwidth-usage> "About storage and bandwidth usage"

1. Git Large File Storage
[https://git-lfs.github.com/](https://git-lfs.github.com/ "Git Large File Storage")

    [Git LFS]: <https://git-lfs.github.com/>
    "Git Large File Storage"

1. irls on YouTube. "Play Video in Unity WebGL" (Mar 25, 2021).
[https://www.youtube.com/watch?v=VKRS9HuIsUY](https://www.youtube.com/watch?v=VKRS9HuIsUY "Play Video in Unity WebGL")
    
    [irls Play Video Unity WebGL]: <https://www.youtube.com/watch?v=VKRS9HuIsUY>
    "Play Video in Unity WebGL"

1. Quora - "Is it possible to play a YouTube video in a Unity3D WebGL project?"
[https://www.quora.com/Is-it-possible-to-play-a-YouTube-video-in-a-Unity3D-WebGL-project](https://www.quora.com/Is-it-possible-to-play-a-YouTube-video-in-a-Unity3D-WebGL-project)

    [Quora play Youtube]: <https://www.quora.com/Is-it-possible-to-play-a-YouTube-video-in-a-Unity3D-WebGL-project>

1. SIMMER.io - "Adding Video to Unity WebGL"
[https://simmer.io/articles/adding-video-to-unity-webgl](https://simmer.io/articles/adding-video-to-unity-webgl "Adding Video to Unity WebGL")

    [SIMMER Adding Video]: <https://simmer.io/articles/adding-video-to-unity-webgl>
    "Adding Video to Unity WebGL"
    
    Test video file on GitHub Pages
    [https://theroccob.github.io/video/video.mp4](https://theroccob.github.io/video/video.mp4)

1. Simmer.io on YouTube. "Export Unity Games to WebGL and Upload them to the Web" (Aug 24, 2018).
[https://www.youtube.com/watch?v=JZqTHjjtQHM](https://www.youtube.com/watch?v=JZqTHjjtQHM "Export Unity Games to WebGL and Upload them to the Web")

    [Simmer.io Export WebGL Upload to Web]: <https://www.youtube.com/watch?v=JZqTHjjtQHM> "Export Unity Games to WebGL and Upload them to the Web"

1. Stack Overflow:

    "Webgl unity build won't play Video" asked by Jz Konain
    [https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video](https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video "unity3d - Webgl unity build won't play Video - Stack Overflow")

    [Stack Overflow WebGL Video]: <https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video> "unity3d - Webgl unity build won't play Video - Stack Overflow"

1. Unity Answers:

    "Is it possible to embed a YouTube video in Unity's WebGL build?"
    [https://answers.unity.com/questions/1531460/is-it-possible-to-embed-a-youtube-video-in-unitys.html](https://answers.unity.com/questions/1531460/is-it-possible-to-embed-a-youtube-video-in-unitys.html)

    [Unity Answers embed YouTube]: <https://answers.unity.com/questions/1531460/is-it-possible-to-embed-a-youtube-video-in-unitys.html>

1. Unity Forum:

    "Video Player on WebGL" thread
    [https://forum.unity.com/threads/video-player-on-webgl.947019/](https://forum.unity.com/threads/video-player-on-webgl.947019/)

    [Unity Forum Video Player WebGL]: <https://forum.unity.com/threads/video-player-on-webgl.947019/>

1. "Webgl unity build won't play Video" asked by Jz Konain on Stack Overflow
[https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video](https://stackoverflow.com/questions/54856356/webgl-unity-build-wont-play-video)
