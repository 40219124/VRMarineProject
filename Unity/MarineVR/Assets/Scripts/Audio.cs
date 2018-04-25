using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;
    AudioSource source;
    int waitTime;
    int audioNo;
    float totalCD = 5.0f;
    float remainingCD;

    void Start()
    {
        source = GetComponent<AudioSource>();
        totalCD = new System.Random().Next(5, 20);
        remainingCD = totalCD;
    }

    void Update()
    {
        remainingCD -= Time.deltaTime;
        if (remainingCD <= 0.0f)
        {
            audioNo = new System.Random().Next(0, clips.Length);
            source.clip = clips[audioNo];
            source.Play();
            totalCD = new System.Random().Next(5, 20);
            remainingCD = totalCD;
        }
    }
}