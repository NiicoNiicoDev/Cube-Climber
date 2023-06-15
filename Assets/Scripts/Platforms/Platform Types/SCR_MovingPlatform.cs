using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MovingPlatform : SCR_BasePlatform
{
    Vector3 startPos;
    Vector3 targetPos;

    [SerializeField] AnimationCurve moveCurve;

    [SerializeField] float offset = 2f;

    enum Direction { Left, Right, Up, Down };

    [SerializeField] Direction direction;

    [SerializeField] float speed = 1f;
    [SerializeField] float startDelay = 0f;

    float timer;

    protected override void Awake()
    {
        platformType = eplatformType.Moving;
        base.Awake(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null, true);
        }
    }

    private void Start()
    {
        startPos = transform.position;

        switch (direction)
        {
            case Direction.Left:
                targetPos = new Vector3(startPos.x - offset, startPos.y, startPos.z);
                break;
            case Direction.Right:
                targetPos = new Vector3(startPos.x + offset, startPos.y, startPos.z);
                break;
            case Direction.Up:
                targetPos = new Vector3(startPos.x, startPos.y + offset, startPos.z);
                break;
            case Direction.Down:
                targetPos = new Vector3(startPos.x, startPos.y - offset, startPos.z);
                break;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > startDelay)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, moveCurve.Evaluate(Mathf.PingPong((timer - startDelay) * speed, 1)));
        } 
        
    }
}
