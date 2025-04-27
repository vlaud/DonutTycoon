using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmPlayer;
    public AudioSource BGMPlayer => bgmPlayer;

    public void PlayOneShot(AudioSource audio, AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    public void PlayOneShot(AudioClip clip)
    {
        bgmPlayer.PlayOneShot(clip);
    }
}
