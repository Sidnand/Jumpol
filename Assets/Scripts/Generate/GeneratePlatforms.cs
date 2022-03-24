using UnityEngine;

public class GeneratePlatforms : MonoBehaviour {

    public static GeneratePlatforms Instance { get; set; }

    [Header("GameObjects")]
    [SerializeField] GameObject platform;
    [SerializeField] GameObject startingPlatform;

    int maxPlatforms = 8;               // Max number of platforms.
    Vector3 lastPos;                    // Last platforms pos.

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        lastPos = startingPlatform.transform.position;
        Generate ();

    }

    /// <summary>
    /// Generates the platforms.
    /// </summary>
    public void Generate () {

        for(var i = 0; i < maxPlatforms; i++) {

            Instantiate (platform, ReturnNewPos(), Quaternion.identity);

        }

    }
    
    /// <summary>
    /// Returns platforms position.
    /// </summary>
    /// <returns>Position of new platfrom</returns>
    public Vector3 ReturnNewPos () {
        var newZPos = lastPos.z + platform.transform.GetChild(2).gameObject.transform.localScale.x;
        var newPos = new Vector3 (lastPos.x,
                                lastPos.y,
                                newZPos);
        lastPos = newPos;

        return newPos;
    }

}
