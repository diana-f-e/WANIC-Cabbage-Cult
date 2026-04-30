using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if sound effect done, die
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
