using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int score = 0;
    bool gameOver = false;
    public Text scoreText;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();
            //print(score);
        }
    }
    public void GameOver()
    {
        PlatformsScript.instance.StopPlatforms();
        StarSpawner.Instance.StopSpawning();
        GameObject.Find("Player").GetComponent<Player>().canMove = false;
        GameObject.Find("Platform").GetComponent<PlatformMoveScript>().DestroyPlatforms();

    }
}
