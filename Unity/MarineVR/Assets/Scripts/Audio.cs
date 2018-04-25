using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
    [SerializeField]
    int waitTime;
    int audioNo;

    void Start()
    {
        StartCoroutine(SoundOut());
    }

    IEnumerator SoundOut()
    {
        while (true)
        {
            audioNo = new System.Random().Next(0,4);
            AudioSource[] audio = GetComponents<AudioSource>();
            audio[audioNo].Play();
            waitTime = new System.Random().Next(60);

            yield return new WaitForSeconds(waitTime);
        }
    }
}