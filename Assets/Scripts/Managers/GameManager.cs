using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }

    [HideInInspector] public bool started = false;              // If game has started or not.

    [Header("Animator Components")]
    [SerializeField] Animator startMenuPanel;
    [SerializeField] Animator gameOverMenuPanel;
    [SerializeField] Animator collectable;

    [Header ("GameObjects")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject reviveBtn;

    [Header ("Text Components")]
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        if (PlayerPrefs.HasKey("Temp Score")) {

            if (PlayerPrefs.GetInt ("Temp Score") == 0) {
                reviveBtn.SetActive (true);
            } else {
                reviveBtn.SetActive (false);
            }

        }

        MaterialManager.LoadData ();

    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame() {

        startMenuPanel.Play ("Start Menu Panel Exit");
        collectable.Play ("Play Game");
        mainMenuPanel.SetActive (true);
        started = true;
        InvokeRepeating ("UpdateScore", 0.3f, 0.3f);

    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame() {

        CancelInvoke ("UpdateScore");

        ScoreManager.Instance.SaveHighScore ();
        CollectableManager.Instance.SaveCollectables ();

        scoreText.text = "Score: " + ScoreManager.Instance.score;
        highScoreText.text = "High Score: " + ScoreManager.Instance.highScore;

        collectable.Play ("End Game");

        gameOverMenuPanel.gameObject.SetActive (true);
        mainMenuPanel.SetActive (false);

    }

    /// <summary>
    /// Resets the game.
    /// </summary>
    public void ResetGame () {

        gameOverMenuPanel.Play ("Game Over Menu Panel Hide");

        if (!gameOverMenuPanel.GetCurrentAnimatorStateInfo (0).IsName ("Game Over Menu Panel Hide")) {
            gameOverMenuPanel.gameObject.SetActive (false);
            SceneManager.LoadScene (0);
        }

    }

    /// <summary>
    /// Updates the score.
    /// </summary>
    private void UpdateScore () {

        ScoreManager.Instance.UpdateScore (1);

    }

}
