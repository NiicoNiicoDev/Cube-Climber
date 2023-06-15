using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_Input : MonoBehaviour
{
    public bool bIsGounded = true;
    public static SCR_Input Instance { get; private set; }
    private GameObject player;
    public Rigidbody playerRB;
    private SCR_JumpTrajectory jumpTrajectory;

    private Vector2? touchOrigin;
    private Vector2? touchPosition;
    private Vector2? touchDelta;

    private float touchAngle;

    private float touchDuration = 0.0f;

    float power = 0f;
    float powerMultiplier = 8f;

    #region Getters/Setters
    public Vector2? TouchOrigin { get { return touchOrigin; } }
    public Vector2? TouchPosition { get { return touchPosition; } }
    public Vector2? TouchDelta { get { return touchDelta; } }

    public float PowerMultiplier { get { return powerMultiplier; }  set { powerMultiplier = value; } }


    public float TouchAngle { get { return touchAngle; } }
    public float TouchDuration { get { return touchDuration; } }
    #endregion
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } 
        else
        {
            Instance = this;
        }

        player = FindObjectOfType<SCR_CharacterMovement>().gameObject;

        jumpTrajectory = player.GetComponent<SCR_JumpTrajectory>();
        jumpTrajectory.lineRenderer.enabled = false;

        playerRB = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchDuration += Time.deltaTime;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (bIsGounded)
                    {
                        jumpTrajectory.lineRenderer.enabled = true;
                        jumpTrajectory.angle = 0;
                        jumpTrajectory.RenderArc();
                    }
                    touchOrigin = touch.position;
                    touchDuration = 0.0f;
                    break;
                    
                case TouchPhase.Moved:

                    touchDelta = touch.position - touchOrigin;
                    touchPosition = touch.position;
                    touchAngle = Mathf.Atan2(touchPosition.Value.y - touchOrigin.Value.y, touchPosition.Value.x - touchOrigin.Value.x);
                    
                    if (bIsGounded)
                    {
                        SetPlayerRotation();

                        power = TouchDelta.Value.magnitude * 0.0015f;
                        if (power > 1) { power = 1; }
                        jumpTrajectory.velocity = powerMultiplier * power;

                        jumpTrajectory.angle = ((Mathf.Rad2Deg * TouchAngle) + 180) % 360;
                        jumpTrajectory.RenderArc();
                    }
                    break;
                    
                case TouchPhase.Stationary:
                    if (bIsGounded) 
                        jumpTrajectory.RenderArc();
                    break;

                case TouchPhase.Ended:
                    if (bIsGounded)
                    {
                        LaunchPlayer();
                    }
                    jumpTrajectory.lineRenderer.positionCount = 0;
                    jumpTrajectory.lineRenderer.enabled = false;
                    touchOrigin = null;
                    touchDelta = null;
                    touchDuration = 0.0f;
                    break;
            }
        }

        if (Physics.OverlapSphere(player.transform.position, 0.2f, jumpTrajectory.floorMask).Length > 0) //(Physics.Raycast(player.transform.position, -Vector3.up, 0.1f, jumpTrajectory.floorMask))
        {
            bIsGounded = true;
        }
        else
        {
            bIsGounded = false;
        }
    }

    void SetPlayerRotation()
    {
        if (TouchDelta.HasValue)
        {
            if (TouchDelta.Value.x > 0)
            {
                player.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                player.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }

    void LaunchPlayer()
    {
        Vector2 targetDirection = new Vector2(touchPosition.Value.x - touchOrigin.Value.x, touchPosition.Value.y - touchOrigin.Value.y); 
        targetDirection.Normalize();
        targetDirection *= -1;

        playerRB.AddForce(targetDirection * (powerMultiplier * power * 50), ForceMode.Force);

        Debug.Log(jumpTrajectory.FlightTime());

        if (jumpTrajectory.FlightTime() > 1.2)
            StartCoroutine(jumpTrajectory.RotatePlayerDuringJump(TouchDelta.Value.x));
    }
}
