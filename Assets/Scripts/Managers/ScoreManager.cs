using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; set; }

    [HideInInspector] public int score = 0;                 // Holds current score.
    [HideInInspector] public int highScore;                 // Players highscore

    [Header("Text Components")]
    [SerializeField] Text scoreText;

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        if (!PlayerPrefs.HasKey("Temp Score")) PlayerPrefs.SetInt ("Temp Score", 0);

        if (!PlayerPrefs.HasKey ("High Score")) PlayerPrefs.SetInt ("High Score", 0);

        UpdateScore(PlayerPrefs.GetInt ("Temp Score"));
        highScore = PlayerPrefs.GetInt ("High Score");

    }

    /// <summary>
    /// Updates the score.
    /// </summary>
    /// <param name="amount">Amount to update score by</param>
    public void UpdateScore(int amount) {

        if (score % 10 == 0 && score != 0) {
            StartCoroutine (MainCamera.Instance.UpdateBackground ());
        }

        score += amount;
        scoreText.text = score.ToString ();

    }

    /// <summary>
    /// Saves the users high score
    /// </summary>
    public void SaveHighScore() {

        if (score > highScore) {
            PlayerPrefs.SetInt ("High Score", score);
            highScore = score;
        }

    }

}
