using UnityEngine;

public class Collectables : MonoBehaviour {

    Vector3 rot;

    private void Start () {

        rot = new Vector3(0, Random.Range (-90, 90), 0);

    }

    private void Update () {

        transform.Rotate (rot * Time.deltaTime);

    }

}
