using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    public int score = 0;

    public static GameManager instance;

    
    private Text scoreUIText;
    private GameObject canvas;
    private GameObject gameOver;
    private GameObject mainMenu;

    private AudioSource music;

    #region Singleton
    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        Time.timeScale = 0;     
        cursorIsLocked(false);
        FindComponents();
    }

    private void FindComponents()
    {
        instance.canvas = GameObject.Find("Canvas");

        instance.mainMenu = canvas.transform.Find("Menu").gameObject;
        instance.gameOver = canvas.transform.Find("GameOver").gameObject;
        instance.scoreUIText = canvas.transform.Find("ScoreText").GetComponent<Text>();

        instance.music = transform.Find("BackgroundMusic").GetComponent<AudioSource>();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        cursorIsLocked(false);
        music.Stop();
    }

    public void cursorIsLocked(bool lockedCur)
    {
        if (lockedCur)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
        instance.mainMenu.SetActive(true);
        cursorIsLocked(false);
    }

    void Update()
    {
        scoreUIText.text = "Score: " + score.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }

    public void StartGame()
    {
        instance.mainMenu.SetActive(false);
        Time.timeScale = 1;
        cursorIsLocked(true);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void Win()
    {

    }
}