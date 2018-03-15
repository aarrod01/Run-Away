using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainCamera))]
public class MainCamera : MonoBehaviour {
    
    Vector2 posicionPuntero;
    Camera c;
    Transform cameraTransform;
    Rigidbody2D playerRb;
    Vector2 velocity;
    public float smoothTime = 1;
    Vector3 offset;
    PlayerMovement playerMovement;
    Vector2 ultimaVelocidad;
    public float distancia = 5f;

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

    // Update is called once per frame
    void Update()
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
