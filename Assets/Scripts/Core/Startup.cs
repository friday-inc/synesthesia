using UnityEngine;

public class Startup : MonoBehaviour
{
    private GameObject game = null;
    [SerializeField]
    private string startScene = "Title";

    public void Awake()
    {
        game = GameObject.Find("Game");

        if (game == null)
        {
            if (Application.loadedLevelName == startScene)
            {
                game = new GameObject("Game");
                GameObject.DontDestroyOnLoad(game);
            }
            else
            {
                Application.LoadLevel(startScene);
            }
        }
    }
}
