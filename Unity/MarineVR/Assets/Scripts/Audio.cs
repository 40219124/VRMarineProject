using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
    [SerializeField]
    int waitTime = 2;

    void Start()
    {
        StartCoroutine(SoundOut());
    }

    IEnumerator SoundOut()
    {
        while (true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();


            yield return new WaitForSeconds(waitTime);
        }
    }
}