using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public Text samplesText;
    private AudioSource source = null;

    private float[] outputData;
    public float BackgroundBeat { get; private set; }

    void Awake()
    {
        outputData = new float[64];
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
            BackgroundBeat = average > 0.05f ? Mathf.Min(average*10.0f, 0.75f) : 0.0f;

            samplesText.text = string.Format("time: {0:000.000}s/{1:000.000}s ({2:00.00}%)\nsamples: {3}\nbeat: {4}", source.time, source.clip.length, (source.time / source.clip.length) * 100.0f, source.timeSamples, BackgroundBeat);
        }
    }
}

