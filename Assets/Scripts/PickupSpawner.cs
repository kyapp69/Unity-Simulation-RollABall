using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// PickupSpawner will place the pickups around the Roll-A-Ball playing area based on the
// scale and numPickups from app params.
public class PickupSpawner: MonoBehaviour {
    private int scale;
    public int numPickups;
    public GameObject pickup;

    public void Awake() {
        numPickups = Mathf.Min(Mathf.Max(0, numPickups), 1000); //clamp 0 - 1000 extra pickups
    }
    public void Start() {
        scale = GameObject.FindGameObjectsWithTag("Environment Spawner")[0].GetComponent<EnvSpawner>().scale;
        int possibilities = 100 * scale - 4; // subtract 1 from each side to keep the spawns inside the walls
        List<Vector2Int> allPoints = new List<Vector2Int>(possibilities);
        int boundary = 10 * scale / 2 - 2;  //scale up by 10 units, origin is at the center
                                            // so offset by half, subtract 1 from each half
        for (int i = -boundary; i < boundary; i++) {
            for (int j = -boundary; j < boundary; j++) {
                allPoints.Add(new Vector2Int(i, j));
            }
        }
        List<Vector2Int> chosenPoints = new List<Vector2Int>();

        for (int i = 0; i < numPickups; i++) {
            if(allPoints.Count < 1) {
                numPickups = i;
                break;
            }
            int ind = Random.Range(0, allPoints.Count-1);
            Vector2Int chosen = allPoints[ind];
            chosenPoints.Add(chosen);
            allPoints.RemoveAt(ind);
        }
        allPoints = null;

        foreach (Vector2Int pos in chosenPoints) {
            Instantiate(pickup, new Vector3(pos[0], 0.5f, pos[1]), Quaternion.identity);
        }
    }
}