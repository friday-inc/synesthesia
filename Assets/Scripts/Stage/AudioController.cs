using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource source = null;

    private float[] outputData;
    public float BackgroundBeat { get; private set; }
    public float PlaybackProgress { get; private set; }
    public float[] OutputData { get; private set; }

    void Awake()
    {
        outputData = new float[1024];
        OutputData = new float[3];
        source = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        source.Play();
    }

    void Update()
    {
        if (source.isPlaying)
        {
            source.GetOutputData(outputData, 0);
            float average = 0.0f;

            foreach (float sample in outputData) average+=Mathf.Abs(sample);
            average /= outputData.Length;
            BackgroundBeat = (average > 0.05f && Mathf.Abs(outputData[32]) > 0.08f) ? Mathf.Min(average*10.0f, 0.75f) : 0.0f;
            OutputData[0] = Mathf.Abs(outputData[32]);
            OutputData[1] = Mathf.Abs(outputData[128]);
            OutputData[2] = Mathf.Abs(outputData[800]);
            PlaybackProgress = (source.time / source.clip.length);
        }
    }
}

