using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrajectory : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] [Range(3, 30)] private int lineSegmentCount = 20;
    private List<Vector3> linePoints = new List<Vector3>();

    public LayerMask layerMask;

    #region Singleton
    public static LineTrajectory Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = FlightDuration / lineSegmentCount;

        linePoints.Clear();
        linePoints.Add(startingPoint);

        for(int i = 1; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in time

            Vector3 MovementVector = new Vector3(
                x: velocity.x * stepTimePassed, 
                y: velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, 
                z: velocity.z * stepTimePassed
                );

            Vector3 NewPointOnLine = -MovementVector + startingPoint;

            RaycastHit hit;
            if (Physics.Raycast(origin: linePoints[i-1], direction: NewPointOnLine - linePoints[i-1], out hit, (NewPointOnLine - linePoints[i - 1]).magnitude, layerMask: layerMask))
            {
                linePoints.Add(hit.point);
                break;
            }

            linePoints.Add(NewPointOnLine);

        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

        //http://hyperphysics.phy-astr.gsu.edu/hbase/traj.html#tracon
    }

    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }

}
