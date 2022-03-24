using UnityEngine;

public class Platform : MonoBehaviour {

    private void Start () {
        Setup ();
    }

    private void Update () {

        if (IsOffScreen ()) {

            if (gameObject.name == "Starting Platform") gameObject.SetActive (false);

            else {

                transform.position = GeneratePlatforms.Instance.ReturnNewPos ();
                Setup ();

            }

        }

    }

    /// <summary>
    /// Sets-up the platform and its childs.
    /// </summary>
    private void Setup () {

        HasChild ("Obstacle");
        HasChild ("Collectable");

    }

    /// <summary>
    /// Checks if object has a child.
    /// </summary>
    /// <param name="childTag">Tag of child</param>
    private void HasChild (string childTag) {

        var randInt = Random.Range (0, 11); // 0 - 10

        foreach (Transform child in transform) {

            if (child.CompareTag (childTag)) {

                if (randInt > 2) child.gameObject.SetActive (true);

                else child.gameObject.SetActive (false);

            }

        }

    }

    /// <summary>
    /// Checks if platform is offscreen.
    /// </summary>
    /// <returns>Whether platform is offscreen</returns>
    private bool IsOffScreen () {

        Vector3 screenPoint = Camera.main.WorldToViewportPoint (transform.position);
        bool onScreen = screenPoint.z > -0.5;

        if (!onScreen) return true;

        else return false;

    }

}
