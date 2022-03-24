using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public static ShopManager Instance { get; set; }

    public GameObject [] materialBtns;                    // List of all material btns

    [SerializeField] Text collectablesTextShop;

    private void Awake () {

        DontDestroyOnLoad (gameObject);

        if (Instance != null && Instance != this) {
            Destroy (gameObject);
        } else {
            Instance = this;
        }

    }

    public void ClickSetup() {
        collectablesTextShop.text = CollectableManager.Instance.collectables.ToString ();
    }

    /// <summary>
    /// Setup the shop
    /// </summary>
    public void Setup() {

        foreach(var i in materialBtns) {

            MaterialButton matManager = i.GetComponent<MaterialButton> ();
            string mat = matManager.mat.name;

            string match = MaterialManager.materials.Find (stringToCheck => stringToCheck.Equals (mat));

            if (match != null) {

                i.transform.GetChild (0).gameObject.SetActive (false);

            }

        }

    }

    /// <summary>
    /// Adds outline to button
    /// </summary>
    public void AddOutline (GameObject o) {

        GameObject [] btns = GameObject.FindGameObjectsWithTag ("Material Button");

        foreach (var btn in btns) {
            btn.GetComponent<Outline> ().enabled = false;
        }

        o.GetComponent<Outline> ().enabled = true;

    }

}
