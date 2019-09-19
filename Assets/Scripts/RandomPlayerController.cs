using UnityEngine;
using System.Collections;

// RandomPlayerController applies random user inputs if the user
// is not supplying inputs. This is useful to move the balls around
// in a random fashion during a cloud simulation.
public class RandomPlayerController : MonoBehaviour {
    private float decisionCadence = 0.2f; //sec
    private float sinceLastDecision = 0.0f;
    private float stateChangeProbability = 0.25f;
    private float maxPressDuration = 0.5f;

    private float horizontalPressDuration = 0.0f;
    private float verticalPressDuration = 0.0f;
    void FixedUpdate () {
        if (sinceLastDecision > decisionCadence) {
            bool horizontalChange = Random.Range(0.0f, 1.0f) > stateChangeProbability;
            if (horizontalChange) {
                if (!InputBroker.HorizontalPressed()) {
                    InputBroker.setHorizontal(Random.Range(-1.0f, 1.0f));
                } else {
                    InputBroker.setHorizontal(0.0f);
                }
            }
            bool verticalChange = Random.Range(0.0f, 1.0f) > stateChangeProbability;
            if (verticalChange) {
                if (!InputBroker.VerticalPressed()) {
                    InputBroker.setVertical(Random.Range(-1.0f, 1.0f));
                } else {
                    InputBroker.setVertical(0.0f);
                }
            }
            sinceLastDecision = 0.0f;
        }

        if (horizontalPressDuration > maxPressDuration) {
            InputBroker.setHorizontal(0.0f);
            sinceLastDecision = 0.0f;
        }
        if (verticalPressDuration > maxPressDuration) {
            InputBroker.setVertical(0.0f);
            sinceLastDecision = 0.0f;
        }
    }

    void Update () {
        sinceLastDecision += Time.deltaTime;
        if (InputBroker.HorizontalPressed()) {
            horizontalPressDuration += Time.deltaTime;
        } else {
            horizontalPressDuration = 0;
        }
        if (InputBroker.VerticalPressed()) {
            verticalPressDuration += Time.deltaTime;
        } else {
            verticalPressDuration = 0;
        }
    }
}