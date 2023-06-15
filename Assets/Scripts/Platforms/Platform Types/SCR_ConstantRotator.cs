using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ConstantRotator : SCR_BasePlatform
{
    enum eRotationDireection
    {
        clockwise,
        counterClockwise
    }

    [SerializeField] private eRotationDireection rotationDirection = eRotationDireection.clockwise;

    [SerializeField] private float rotationsPerSecond;

    protected override void Awake()
    {
        platformType = eplatformType.ConstantRotator;
        base.Awake();
    }

    private void Update()
    {
        RotatePlatform();
    }

    void RotatePlatform()
    {
        switch (rotationDirection)
        {
            case eRotationDireection.clockwise:
                transform.Rotate(0, 0, (-rotationsPerSecond * 360) * Time.deltaTime);
                break;

            case eRotationDireection.counterClockwise:
                transform.Rotate(0, 0, (rotationsPerSecond * 360) * Time.deltaTime);
                break;
        }
    }
}
