using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float smoothSpeed;


    [SerializeField] Vector3 offset;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = Player.position + offset;
        Vector3 smothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smothPosition;
    }
}
