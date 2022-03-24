using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; set; }

    [Header("GameObjects")]
    [SerializeField] GameObject instructions;

    Rigidbody rb;
    Renderer rend;

    float speed = 690;              // Speed of player.
    float thrust = 40;              // Jump force.
    float gravity = -6.18f;         // Gravity force.

    bool isGrounded = false;        // If on ground or not.
    bool jump = false;              // If player can jump.

    private void Awake () {

        Instance = this;

    }

    private void Start () {
        
        rb = GetComponent<Rigidbody> ();
        rend = GetComponent<Renderer> ();

        StartMaterial ();

    }

    private void Update () {

        if (Physics.Raycast (transform.position, -Vector3.up, 0.4f)) isGrounded = true;
        else isGrounded = false;

        if (isGrounded) {

            if (Application.platform == RuntimePlatform.WindowsEditor) DesktopControls ();

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.Android) MobileControls ();

        }

    }

    private void FixedUpdate () {

        if (GameManager.Instance.started) {

            Move ();
            Gravity ();

            if (jump) {

                if (instructions.activeSelf) instructions.SetActive (false);

                if (isGrounded) {
                    rb.velocity += new Vector3 (0, thrust, 0);

                    SoundManager.Instance.PlayOneShot (SoundManager.Instance.jump);

                    jump = false;
                }

            }

        }

    }

    private void OnCollisionEnter (Collision collision) {

        if (collision.gameObject.tag == "Obstacle") {

            if (instructions.activeSelf) instructions.SetActive (false);

            gameObject.SetActive (false);
            GameManager.Instance.EndGame ();

        }

    }

    private void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Collectable") {

            SoundManager.Instance.PlayOneShot (SoundManager.Instance.collect);
            CollectableManager.Instance.UpdateCollectables (1);

            other.gameObject.SetActive (false);

        }

    }

    /// <summary>
    /// Controls for mobile devices.
    /// </summary>
    private void MobileControls () {

        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) jump = true;

    }

    /// <summary>
    /// Controls for desktops.
    /// </summary>
    private void DesktopControls () {

        if (Input.GetMouseButtonDown (0)) jump = true;

    }

    /// <summary>
    /// Adds gravity.
    /// </summary>
    private void Gravity () {

        rb.velocity += new Vector3 (0, gravity, 0);

    }

    /// <summary>
    /// Moves player.
    /// </summary>
    private void Move () {

        rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, speed * Time.deltaTime);

    }

    /// <summary>
    /// Changes the player material.
    /// </summary>
    /// <param name="mat">Material</param>
    public void ChangeMaterial (Material mat) {

        rend.sharedMaterial = mat;

    }

    /// <summary>
    /// Sets starting material
    /// </summary>
    public void StartMaterial() {

        if (!PlayerPrefs.HasKey ("Current Material")) PlayerPrefs.SetString ("Current Material", "Default");

        foreach (var i in ShopManager.Instance.materialBtns) {

            MaterialButton matManager = i.GetComponent<MaterialButton> ();
            string mat = matManager.mat.name;

            if (mat == PlayerPrefs.GetString("Current Material")) {

                ChangeMaterial (matManager.mat);

                ShopManager.Instance.AddOutline (i);

            }

        }

    }

}
