using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SnapRotator : SCR_BasePlatform
{
    enum eRotationDireection
    {
        clockwise,
        counterClockwise
    }

    [SerializeField] private float snapDelay;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private eRotationDireection rotationDirection = eRotationDireection.clockwise;

    bool bIsRotating = false;

    [SerializeField] private float timer;
    [SerializeField] float routineTimer = 0f;

    protected override void Awake()
    {
        platformType = eplatformType.SnapRotator;
        base.Awake();
    }

    private void Update()
    {

    }
}
