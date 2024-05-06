using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public static class VideoImporter
{
    public static VideoClip LoadVideo(string path)
    {
        return Resources.Load<VideoClip>(path);
    }
}
