using UnityEngine;
using System.Collections;
using Unity.AI.Simulation;

// PlayerSpawner will spawn players in a spiral near the center of the
// Roll-A-Ball playing field.
// Cameras will be attached to players if the extraCameras value is > 0.
// There cannot be more Cameras than players to attach them to.
public class PlayerSpawner: MonoBehaviour {
    public int extraPlayers = 0;
    public int extraCameras = 0;
    private float playerStartHeight = 0.5f;
    private float cameraStartHeight = 4.0f;
    private float cameraOffsetRange = 5.0f;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public void Awake() {
        extraPlayers = Mathf.Min(Mathf.Max(0, extraPlayers), 1000); //clamp 0 - 1000 extra players
        extraCameras = Mathf.Min(Mathf.Max(0, extraCameras), 1000); //clamp 0 - 1000 extra cameras
    }
    public void Start() {
        int remainingCameras = extraCameras;
        int expectedCams = Mathf.Max(1, Mathf.Min(extraPlayers+1, extraCameras+1));
        Camera[] cams = new Camera[expectedCams];
        cams[0] = GameObject.FindGameObjectsWithTag("Data Capture")[0].GetComponent<CameraGrab>()._cameraSources[0];
        cams[0].name = "Camera0";
        int camIter = 1;

        float arc = 1.2f;
        float separation = 1.2f;

        float r = arc;
        float b = separation / (2 * Mathf.PI);

        float phi = r / b;

        for (int i = 0; i <= extraPlayers; i++) {
            if (i == 0){
                // player at Vector3(0, 0.5, 0) is in the scene with the camera attached. skip first instantiation.
                continue;
            }
            Vector2 point = p2c(r, phi);
            GameObject newPlayer = Instantiate(playerPrefab, new Vector3(point[0], playerStartHeight, point[1]), Quaternion.identity);
            if (remainingCameras > 0) {
                float randXOffset = Random.Range(-cameraOffsetRange, cameraOffsetRange);
                float randZOffset = Random.Range(-cameraOffsetRange, cameraOffsetRange);
                GameObject newCamera = Instantiate(cameraPrefab, new Vector3(point[0]+randXOffset, cameraStartHeight, point[1]+randZOffset), Quaternion.identity);
                newCamera.name = "Camera" + camIter;
                var cam = newCamera.GetComponent<Camera>();
                cam.enabled = false;
                cam.targetTexture = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 24, RenderTextureFormat.ARGB32);
                cams[camIter] = cam;
                newCamera.GetComponent<CameraController>().player = newPlayer;
                remainingCameras--;
                camIter++;
            }
            phi += arc / r;
            r = b * phi;
        }
        GameObject.FindGameObjectsWithTag("Data Capture")[0].GetComponent<CameraGrab>()._cameraSources = cams;
    }
    private Vector2 p2c(float r, float phi) {
        return new Vector2(r * Mathf.Cos(phi), r * Mathf.Sin(phi));
    }
}