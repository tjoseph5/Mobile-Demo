using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class DragAndShoot : MonoBehaviour
{
    public float power;
    public float maxDrag;
    public Rigidbody rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

        lr.SetPosition(0, gameObject.transform.position);
    }

    private void Update()
    {

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                DragStart();

            }

            if(touch.phase == TouchPhase.Moved)
            {
                Dragging();

            }

            if(touch.phase == TouchPhase.Ended)
            {
                DragRelease();

            }
        }

    }

    void DragStart()
    {
        dragStartPos = touch.position;
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    void Dragging()
    {
        Vector3 draggingPos = touch.position;
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    void DragRelease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = touch.position;
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode.Impulse);
    }

}
