using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax gameObject
    Vector2 startingPosition;

    //Start Z value of the parallax gameObject
    float startingZ;

    // Distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // If object is in front of target, use nearClipPlane. If behind target, user farClipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The further the object from the player, the faster the parallaxEffect object will move. Drag it's Z value closer to the target to make it move slower.
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor, but z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
