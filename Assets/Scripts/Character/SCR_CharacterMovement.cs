using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CharacterMovement : MonoBehaviour
{
    public bool grounded = true;

    public float powerMulti = 8f;

    public Vector3 RBVelocity;
    
    private void Awake()
    {
        
    }
    
    private void Start()
    {
    }

    private void Update()
    {
        grounded = SCR_Input.Instance.bIsGounded;
        SCR_Input.Instance.PowerMultiplier = powerMulti;

        RBVelocity = SCR_Input.Instance.playerRB.velocity;
    }
}
