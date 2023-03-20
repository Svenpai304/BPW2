using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float moveTime;
    
    public Vector3 movementDestination;
    private float moveInterpolation;

    public void Start()
    {
        movementDestination = transform.position;
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(transform.position - Vector3.forward);
    }

    public void Move(Vector3 destination)
    {
        movementDestination = destination;
    }

    private void FixedUpdate()
    {
        if (transform.position != movementDestination)
        {
            transform.position = Vector3.Lerp(transform.position, movementDestination, moveInterpolation);
            moveInterpolation += Time.deltaTime / moveTime;
        }
        else moveInterpolation = 0;
    }
}
