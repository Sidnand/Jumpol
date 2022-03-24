using System.Collections;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public static MainCamera Instance { get; set; }

    [Header("GameObjects")]
    [SerializeField] GameObject player;

    Vector3 offset;             // Distance from camera to player.
    Camera cam;                 // Camera component.

    Color [] colors = new Color [] {

        new Color (0.17f, 0.82f, 0.91f), // Light Blue
        new Color (0.19f, 0.39f, 0.72f), // Dark Blue
        new Color(0.90f, 0.50f, 0.50f), // Light Red
        new Color (1, 0.91f, 0.60f), // Light Yellow
        new Color (0.68f, 1, 0.47f), // Light Green
        new Color (0.96f, 0.52f, 1), // Purple
        new Color (1, 0.40f, 0.63f) // Pink

    };

    int previousColor;
    int currentColor;

    private void Awake () {
        Instance = this;
    }

    private void Start () {

        cam = GetComponent<Camera> ();

        offset = transform.position - player.transform.position;
        currentColor = Random.Range (0, colors.Length);

        StartCoroutine (UpdateBackground ());

    }

    private void LateUpdate () {

        Vector3 pos = new Vector3 (player.transform.position.x + offset.x,
                                transform.position.y,
                                player.transform.position.z + offset.z);

        transform.position = pos;

    }

    /// <summary>
    /// Changes the background color
    /// </summary>
    /// <returns>NULL</returns>
    public IEnumerator UpdateBackground () {

        float t = 0;

        previousColor = currentColor;
        currentColor = Random.Range (0, colors.Length);

        while (t <= 1f) {

            cam.backgroundColor = Color.Lerp (colors [previousColor], colors [currentColor], t);

            t += Time.deltaTime;

            yield return null;

        }
    }

}
