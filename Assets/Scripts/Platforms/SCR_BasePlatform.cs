using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BasePlatform : MonoBehaviour
{
    public enum eplatformType
    {
        Stationary,
        ConstantRotator,
        SnapRotator,
        Moving,
        OneWay
    }

    protected eplatformType platformType = eplatformType.Stationary;

    Vector3 position;

    public eplatformType PlatformType { get { return platformType; } }

    protected virtual void Awake()
    {
        position = transform.position;

        transform.name = $"Platform : {platformType} >> X:{position.x} Y:{position.y}";
    }
}
