using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// RollABallAppParams represent a deserialized version of our app Params.
// This is required to transform the app_params.json into a Class instantiation.
// Keys and value types in app_params.json should match these variables.
[System.Serializable]
public class RollABallAppParams
{
    public int extraPlayers;
    public int scale;
    public int numPickups;
    public int extraCameras;
    public float quitAfterSeconds;
    public float forceCrashAfterSeconds;
    public float screenCaptureInterval;
    public static bool AreAllDefault(RollABallAppParams rabParams)
    {
        if(rabParams.extraPlayers == 0
            && rabParams.scale == 0
            && rabParams.numPickups == 0
            && rabParams.extraCameras == 0
            && rabParams.quitAfterSeconds == 0
            && rabParams.forceCrashAfterSeconds == 0
            && rabParams.screenCaptureInterval == 0)
        {
            return true;
        }
        return false;
    }
}