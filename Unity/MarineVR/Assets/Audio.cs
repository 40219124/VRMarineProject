using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
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


            yield return new WaitForSeconds(2);
        }
    }
}