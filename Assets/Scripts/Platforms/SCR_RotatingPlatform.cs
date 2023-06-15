using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RotatingPlatform : SCR_BasePlatform
{
    public float rotationSpeed;
    public float snapDelay;
    public float snapRotationAmount;

    Coroutine snapRoutine;

    public enum RotationType
    {
        continous,
        snapping
    }

    public RotationType rotationType;

    private void Awake()
    {
        
    }

    public void Update()
    {
        switch (rotationType)
        {
            case RotationType.continous:
                ContinousRotation();
                break;

            case RotationType.snapping:
                if (snapRoutine != null)
                {
                    StartCoroutine(SnapRotation());
                }
                break;
        }
    }

    void ContinousRotation()
    {
        transform.Rotate(0, 0, rotationSpeed * rotationSpeed * Time.deltaTime);
    }

    IEnumerator SnapRotation()
    {
        transform.Rotate(0, 0, snapRotationAmount);

        yield return new WaitForSeconds(snapDelay);
    }
}
