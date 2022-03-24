using UnityEngine;
using UnityEngine.UI;

public class MaterialButton : MonoBehaviour {

    Button btn;

    public Material mat;                    // Material
    [SerializeField] int price;             // Price of material

    private void Start () {

        btn = GetComponent<Button> ();

        btn.onClick.AddListener (CheckMaterial);
        
    }

    /// <summary>
    /// Changes the players material
    /// </summary>
    public void CheckMaterial () {

        // If not default game object
        if (gameObject.name != "Default") {

            // If materials list is empty (Player hasn't bought any materials)
            if (MaterialManager.materials == null) {

                Buy ();

            } else {

                // See if materials list has current material in it
                string match = MaterialManager.materials.Find (stringToCheck => stringToCheck.Equals (mat.name));

                // If it doesn't
                if (match == null) {

                    // Buy it
                    Buy ();
                    // If it does
                } else if (match != null) {

                    SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);

                    // Set it
                    Player.Instance.ChangeMaterial (mat);

                    PlayerPrefs.SetString ("Current Material", mat.name);

                    ShopManager.Instance.AddOutline (gameObject);

                }
            }

            // If material is default
        } else if (gameObject.name == "Default") {

            SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);

            // Set it
            Player.Instance.ChangeMaterial (mat);
            PlayerPrefs.SetString ("Current Material", mat.name);

            ShopManager.Instance.AddOutline (gameObject);

        }

    }

    /// <summary>
    /// Buy new material
    /// </summary>
    private void Buy() {

        // If player can afford it
        if (CollectableManager.Instance.collectables >= price) {

            SoundManager.Instance.PlayOneShot (SoundManager.Instance.buy);

            MaterialManager.materials.Add (mat.name);

            // Change the material
            ChangeMaterial ();

        } else {
            SoundManager.Instance.PlayOneShot (SoundManager.Instance.cantBuy);
        }

    }

    /// <summary>
    /// Change material
    /// </summary>
    private void ChangeMaterial() {

        CollectableManager.Instance.collectables -= price;
        CollectableManager.Instance.SaveCollectables ();
        CollectableManager.Instance.DisplayCollectables ();
        
        MaterialManager.SaveData (mat.name);
        Player.Instance.ChangeMaterial (mat);

        ShopManager.Instance.ClickSetup ();

        gameObject.transform.GetChild (0).gameObject.SetActive (false);

        PlayerPrefs.SetString ("Current Material", mat.name);

        ShopManager.Instance.AddOutline (gameObject);

    }

}
