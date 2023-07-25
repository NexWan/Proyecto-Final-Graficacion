using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponentInChildren<VideoPlayer>();

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}