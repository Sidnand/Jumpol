using UnityEngine;
using UnityEngine.UI;

public class ButtonClickManager : MonoBehaviour {

    public static ButtonClickManager Instance { get; set; }
    GameObject shop;
    GameObject shopBackbtn;

    private void Awake () {
        Instance = this;
    }

    private void Start () {
        shop = GameObject.Find ("Shop Menu Panel");
        shopBackbtn = GameObject.Find ("Back");

        Button btn = shopBackbtn.GetComponent<Button> ();
        btn.onClick.AddListener (ExitShop);
    }

    /// <summary>
    /// Resets game.
    /// </summary>
    public void ResetGame () {

        PlayerPrefs.SetInt ("Temp Score", 0);
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        GameManager.Instance.ResetGame ();

    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void PlayGame () {

        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        GameManager.Instance.StartGame ();

    }

    /// <summary>
    /// Plays animation to show shop.
    /// </summary>
    public void ShowShop () {

        shop.gameObject.transform.parent.GetComponent<ShopManager> ().ClickSetup ();

        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        shop.GetComponent<Animator> ().Play ("Shop Menu Panel Show");

    }

    /// <summary>
    /// Plays animation to exit shop.
    /// </summary>
    public void ExitShop () {

        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        shop.GetComponent<Animator> ().Play ("Shop Menu Panel Hide");

    }

    /// <summary>
    /// Shows and ad for collectables
    /// </summary>
    public void ShowAdCollectable() {

        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        AdManager.Instance.ShowRewardedVideo ();

    }

    /// <summary>
    /// Shows and ad for revive
    /// </summary>
    public void Revive () {

        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);

        if (CollectableManager.Instance.collectables >= 20) {
            CollectableManager.Instance.UpdateCollectables (-20);
            CollectableManager.Instance.SaveCollectables ();
            CollectableManager.Instance.DisplayCollectables ();

            PlayerPrefs.SetInt ("Temp Score", ScoreManager.Instance.score);
            GameManager.Instance.ResetGame ();
        }

    }

    /// <summary>
    /// Closes
    /// </summary>
    public void Close(GameObject close) {
        close.SetActive (false);
    }

}
