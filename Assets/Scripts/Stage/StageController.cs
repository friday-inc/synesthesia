using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour
{
    public PlayerController playerController;
    public AudioController audioController;

    public MeshRenderer backgroundPlaneRenderer;

    void Awake()
    {
    }

    void Update()
    {
        backgroundPlaneRenderer.material.SetFloat("_Beat", audioController.BackgroundBeat);
    }
}
