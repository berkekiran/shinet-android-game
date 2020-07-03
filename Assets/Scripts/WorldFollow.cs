using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFollow : MonoBehaviour
{
    public Transform player;
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 5f;
    public float YMaxValue = 0;
    public float YMinValue = 0;
    public float XMaxValue = 0;
    public float XMinValue = 0; 

    void FixedUpdate() {
        
        Vector3 playerPos = player.position * -2;
        
        if(transform.position.x > XMinValue && transform.position.x < XMaxValue && transform.position.y > YMinValue && transform.position.y < YMaxValue)
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);
        
    }
}
