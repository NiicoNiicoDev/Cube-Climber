using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MovingPlatform : MonoBehaviour
{
    Vector3 startPos;
    Vector3 targetPos;

    [SerializeField] float offset = 2f;

    enum Direction { Left, Right, Up, Down };

    [SerializeField] Direction direction;

    [SerializeField] float speed = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
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

    private void Update()
    {
        transform.position = Vector3.Lerp(startPos, targetPos, Mathf.PingPong(Time.time * speed, 1f));
    }
}
