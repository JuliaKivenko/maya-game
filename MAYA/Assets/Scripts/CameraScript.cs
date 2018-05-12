using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;

    public Transform player;

	void Update () {

       transform.position = new Vector3(
       Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
       Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
       -10);

    }
}
