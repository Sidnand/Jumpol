using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {
    
    [Header("GameObjects")]
    [SerializeField] private GameObject errorPanel;

    public static AdManager Instance { get; set; }

    string gameID;

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        if (Application.platform == RuntimePlatform.Android) {
            gameID = "1591167";
        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            gameID = "1591168";
        } else {
            gameID = "unexpected_platform";
        }

        Advertisement.Initialize (gameID);

    }

    /// <summary>
    /// Shows rewarded video ad
    /// </summary>
    public void ShowRewardedVideo() {

        if (Advertisement.isInitialized) {
            if (Advertisement.IsReady ()) {
                Advertisement.Show ("", new ShowOptions () { resultCallback = HandleShowResult });
            } else {
                errorPanel.SetActive (true);
            }
        } else {
            errorPanel.SetActive (true);
        }

    }

    /// <summary>
    /// Handles the results.
    /// </summary>
    /// <param name="result">Return from ads</param>
    private void HandleShowResult (ShowResult result) {

        switch (result) {

            case ShowResult.Finished:
                PlayerPrefs.SetInt ("Temp Score", 0);
                CollectableManager.Instance.UpdateCollectables (10);
                CollectableManager.Instance.SaveCollectables ();
                CollectableManager.Instance.DisplayCollectables ();
                SceneManager.LoadScene (0);
                break;

            case ShowResult.Skipped:
                PlayerPrefs.SetInt ("Temp Score", 0);
                SceneManager.LoadScene (0);
                break;

            case ShowResult.Failed:
                PlayerPrefs.SetInt ("Temp Score", 0);
                SceneManager.LoadScene (0);
                break;

        }

    }


}
