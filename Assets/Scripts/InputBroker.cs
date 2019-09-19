using UnityEngine;
using System.Collections;

// Helper class to enable random input while still permitting player override.
public class InputBroker {
    private static float currentHorizontal = 0;
    private static float currentVertical = 0;
    public static float GetAxis(string axisName) {
        float inputAxisMovement = Input.GetAxis (axisName);
        if (inputAxisMovement != 0) {
            return inputAxisMovement;
        }
        if (axisName == "Horizontal") {
            return currentHorizontal;
        } else if (axisName == "Vertical") {
            return currentVertical;
        }
        return inputAxisMovement;
    }
    public static bool HorizontalPressed() {
        return currentHorizontal != 0;
    }
    public static bool VerticalPressed() {
        return currentVertical != 0;
    }
    public static void setHorizontal(float value) {
        currentHorizontal = value;
    }
    public static void setVertical(float value) {
        currentVertical = value;
    }
}