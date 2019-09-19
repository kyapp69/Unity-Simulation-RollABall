using UnityEngine;
using System.Collections;
public class EnvSpawner: MonoBehaviour {
    public GameObject ground; // Instantiate ground and walls at scene based on scale.
    public GameObject wall;

    // Original Roll-A-Ball scale is 4, this will increase/decrease the size of the Roll-A-Ball play area.
    public int scale;
    public void Awake() {
        scale = Mathf.Min(Mathf.Max(0, scale), 100); //clamp scale to 0 - 100
    }
    public void Start() {

        Vector3 origin = new Vector3(0, 0, 0);
        GameObject groundObj = Instantiate(ground, origin, Quaternion.identity);
        groundObj.transform.localScale = new Vector3(scale, 1, scale);

        GameObject northWall = Instantiate(wall, origin, Quaternion.identity);
        northWall.transform.position = new Vector3(0, 0, scale * 10 / 2);
        northWall.transform.localScale = new Vector3(scale * 10 + 0.5f, 1, 0.5f);

        GameObject southWall = Instantiate(wall, origin, Quaternion.identity);
        southWall.transform.position = new Vector3(0, 0, -scale * 10 / 2);
        southWall.transform.localScale = new Vector3(scale * 10 + 0.5f, 1, 0.5f);

        GameObject westWall = Instantiate(wall, origin, Quaternion.identity);
        westWall.transform.position = new Vector3(-scale * 10 / 2, 0, 0);
        westWall.transform.localScale = new Vector3(0.5f, 1, scale * 10 + 0.5f);

        GameObject eastWall = Instantiate(wall, origin, Quaternion.identity);
        eastWall.transform.position = new Vector3(scale * 10 / 2, 0, 0);
        eastWall.transform.localScale = new Vector3(0.5f, 1, scale * 10 + 0.5f);
    }
}