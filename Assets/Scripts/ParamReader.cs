using System.Collections;
using System.Collections.Generic;
using Unity.AI.Simulation;
using UnityEngine;

using UnityEngine.Diagnostics;

// Attach ParamReader to a GameObject in the scene to read our application parameters.
public class ParamReader : MonoBehaviour
{
    private bool dieIfNoAppParams = true;
    private RollABallAppParams appParams;
    void Start()
    {
        // Check whether we are in the editor or a cloud simulation run.
        // Read the local app_params.json from the Assets directory if we are in the editor.
        if (!Configuration.Instance.IsSimulationRunningInCloud()){
            Configuration.Instance.SimulationConfig.app_param_uri = "file://" + Application.dataPath + "/StreamingAssets/app_params.json";
        }

        // Retrieve the app params using the DataCapture SDK.
        appParams = Configuration.Instance.GetAppParams<RollABallAppParams>();

        // For this example we want to make sure app params were actually read.
        if (dieIfNoAppParams && (appParams == null || RollABallAppParams.AreAllDefault(appParams)))
        {
            Utils.ForceCrash(ForcedCrashCategory.Abort);
        }

        if(appParams != null)
        {
            // Use the retrieved app params to set our values accordingly.
            float screenCaptureInterval = Mathf.Min(Mathf.Max(0, appParams.screenCaptureInterval), 100.0f);

            GameObject.FindGameObjectsWithTag("Environment Spawner")[0].GetComponent<EnvSpawner>().scale = appParams.scale;
            GameObject.FindGameObjectsWithTag("Player Spawner")[0].GetComponent<PlayerSpawner>().extraPlayers = appParams.extraPlayers;
            GameObject.FindGameObjectsWithTag("Player Spawner")[0].GetComponent<PlayerSpawner>().extraCameras = appParams.extraCameras;
            GameObject.FindGameObjectsWithTag("Pickup Spawner")[0].GetComponent<PickupSpawner>().numPickups = appParams.numPickups;
            GameObject.FindGameObjectsWithTag("Data Capture")[0].GetComponent<CameraGrab>()._screenCaptureInterval = screenCaptureInterval;
            PickupStats.quitAfterSeconds = appParams.quitAfterSeconds;
            PickupStats.forceCrashAfterSeconds = appParams.forceCrashAfterSeconds;
        }
    }
}
