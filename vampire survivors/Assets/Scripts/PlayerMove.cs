using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector]
    public float lastHorizontalDecoupledVector;
    [HideInInspector]
    public float lastVerticalDecoupledVector;
    public float lastHorizontalcoupleVector;
    [HideInInspector]
    public float lastVerticalcoupleVector;

    [HideInInspector]
     public Vector3 movementVector;
    Animate animate;
    [SerializeField] float speed = 3f;
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void Start() {
        lastHorizontalDecoupledVector = -1f;
        lastVerticalDecoupledVector = 1f ;
        lastHorizontalcoupleVector = -1f;
        lastVerticalcoupleVector = 1f;
    }
    

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        if (movementVector.x != 0 || movementVector.y != 0)
        {
            lastHorizontalcoupleVector = movementVector.x;
            lastVerticalcoupleVector = movementVector.y;

        }
        if (movementVector.x != 0)
        {
            lastHorizontalDecoupledVector = movementVector.x;
        }
        if (movementVector.y != 0)
        {
            lastVerticalDecoupledVector = movementVector.y;
        }
        animate.horizontal = movementVector.x;
        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
    public Vector2 GetDirection()
    {
        return new Vector2(lastHorizontalDecoupledVector, lastVerticalDecoupledVector).normalized;
    }
}
