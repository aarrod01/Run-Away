using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainCamera))]

public class MainCamera : MonoBehaviour 
{
	public float smoothTime = 1;
	public float distancia = 5f;

    Camera c;
    Transform cameraTransform;
    Rigidbody2D playerRb;
	PlayerMovement playerMovement;
	Vector2 velocity, posicionPuntero;
    Vector3 offset;

    // Use this for initialization
    void Start () {
 
        c = Camera.main;
        cameraTransform = GetComponent<Transform>();
        GameObject player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
        velocity = Vector2.zero;
        offset = cameraTransform.position + player.transform.position ;
        offset.x = 0;
        offset.y = 0;

    }
		
    void LateUpdate()
    {
        if (((Vector2)cameraTransform.position - playerRb.position).sqrMagnitude > distancia 
            && playerRb.velocity.sqrMagnitude > 0f)
        {
            cameraTransform.position += (Vector3)playerRb.velocity * Time.deltaTime;
            velocity = playerRb.velocity;
        }
        else
        {
            cameraTransform.position = (Vector3)Vector2.SmoothDamp(cameraTransform.position,
                playerRb.position, ref velocity, smoothTime, float.MaxValue, Time.deltaTime) + offset;
        }
    }
}
