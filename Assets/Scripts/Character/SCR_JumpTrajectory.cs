using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_JumpTrajectory : MonoBehaviour
{
    [HideInInspector] public LineRenderer lineRenderer;

    [HideInInspector] public Vector3 startPos;
    
    [HideInInspector] public float velocity;
    [HideInInspector] public float angle;
    public int resolution;

    RaycastHit hit;

    Vector3[] points;

    [SerializeField] public LayerMask floorMask;

    float g;
   
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }

    private void OnValidate()
    {
        if (lineRenderer != null && Application.isPlaying)
        {
            RenderArc();
        }
    }

    private void Start()
    {
        RenderArc();
    }

    private void Update()
    {
    }

    public void RenderArc()
    {
        startPos = transform.position;
        lineRenderer.positionCount = CalculateArcArray().Length;
        lineRenderer.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        int iterations = 0;
        Vector3[] tempArray = new Vector3[resolution + 1];

        float radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            tempArray[i] = CalculateArcPoint(t, maxDistance, radianAngle);

            Collider[] test = new Collider[0];

            if (i > 5)
            {
                if (Physics.OverlapSphere(tempArray[i], 0.05f, floorMask).Length > 0)
                {
                    iterations = i;
                    break;
                }
            }
                
            
        }

        if (iterations == 0) { iterations = resolution + 1; }

        Vector3[] arcArray = new Vector3[iterations];

        for (int i = 0; i < iterations; i++)
        {
            arcArray[i] = tempArray[i];
        }

        points = arcArray;
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance, float radianAngle)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return startPos + new Vector3(x, y);
    }

    private void OnDrawGizmos()
    {
        /*foreach (var item in points)
        {
            Gizmos.DrawWireSphere(item, 0.1f);
        }*/
    }
}
