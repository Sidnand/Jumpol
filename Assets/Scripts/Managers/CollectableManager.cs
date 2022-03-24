using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour {

    public static CollectableManager Instance { get; set; }
    
    public int collectables;              // Holds number of collectables.

    [Header("Text Components")]
    [SerializeField] Text collectablesText;
    [SerializeField] Text collectablesTextShop;

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        if (!PlayerPrefs.HasKey ("Collectables")) PlayerPrefs.SetInt ("Collectables", 0);
        collectables = PlayerPrefs.GetInt ("Collectables");

        DisplayCollectables ();

    }

    /// <summary>
    /// Updates the collectables.
    /// </summary>
    /// <param name="amount">Amount to update score by</param>
    public void UpdateCollectables (int amount) {

        collectables += amount;
        DisplayCollectables ();

    }

    /// <summary>
    /// Display collectables on screen
    /// </summary>
    public void DisplayCollectables () {

        collectablesText.text = collectables.ToString ();

    }

    /// <summary>
    /// Save collectables
    /// </summary>
    public void SaveCollectables() {

        PlayerPrefs.SetInt ("Collectables", collectables);

    }

}
