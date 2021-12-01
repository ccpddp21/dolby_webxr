using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Camera TargetCamera;
    public Camera SecondaryCamera;
    public bool MatchRotation = true;

    private Vector3 pos;

    void Update() {
        if(TargetCamera && TargetCamera.enabled) {
            transform.position = TargetCamera.gameObject.transform.position;

            if(MatchRotation) {
                transform.rotation = TargetCamera.gameObject.transform.rotation;
            }
        }
        else if (SecondaryCamera && SecondaryCamera.enabled)
        {
            pos = SecondaryCamera.gameObject.transform.position;
            pos.x = 0.032f;
            transform.position = pos;

            if (MatchRotation)
            {
                transform.rotation = SecondaryCamera.gameObject.transform.rotation;
            }
        }
    }
}