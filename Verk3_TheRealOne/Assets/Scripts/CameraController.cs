using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 Offset;
    private Vector3 moveVector;

    //Camera Animations
    private float transition = 0.0f;
    private float animDuration = 3.0f;
    private Vector3 animOffset = new Vector3(0, 4, 2);


    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAt.position + Offset;
        // x
        moveVector.x = 0;
        // y
        moveVector.y = Mathf.Clamp(moveVector.y, 3,5);

        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            //Animation í byrjun leiks
            transform.position = Vector3.Lerp(moveVector + animOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animDuration;
            transform.LookAt (lookAt.position + Vector3.up);
        }
    }
}
