using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public PlayerController playerController;
    public AudioController audioController;
    public Text beats;

    public MeshRenderer backgroundPlaneRenderer;

    public Slider progressBar;
    private Color[] colors;
    private float[] tiles;
    private int color = 0;
    private int tile = 0;
    private float lastColorChange = 0.0f;

    void Awake()
    {
        colors = new Color[4] {
            new Color(0.7f, 0.5f, 0.2f, 0.1f),
            new Color(0.2f, 0.5f, 0.7f, 0.1f),
            new Color(0.5f, 0.7f, 0.2f, 0.1f),
            new Color(0.2f, 0.7f, 0.5f, 0.1f),
        };

        tiles = new float[4] { 32.0f, 16.0f, 64.0f, 8.0f };
    }

    void Update()
    {
        backgroundPlaneRenderer.material.SetFloat("_Beat", audioController.BackgroundBeat);
        backgroundPlaneRenderer.material.SetFloat("_Tiles", tiles[tile]);
        backgroundPlaneRenderer.material.SetColor("_Color", colors[color]);

        if (Time.time > lastColorChange + 2.0f && audioController.BackgroundBeat > 0.7f)
        {
            lastColorChange = Time.time;
            if (Random.Range(0, 100) > 50)
            {
                color++;
                color = color % 4;
            }
            else
            {
                tile++;
                tile = tile % 4;
            }
        }
        beats.text = string.Format("{0}\n{1:0.000}, {2:0.000}, {3:0.000}", audioController.BackgroundBeat,
                audioController.OutputData[0], audioController.OutputData[1], audioController.OutputData[2]);

        progressBar.value = audioController.PlaybackProgress;
    }
}
