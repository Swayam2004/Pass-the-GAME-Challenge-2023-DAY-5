using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Anchor : Linkable {
    public const int LAYER = 9;
    public bool canMove;
    public Transform[] otherPoints;

    Vector3 startPoint;
    Vector3 target;
    int pointPos;

    private void Start()
    {
        links = new List<Link>();
        if (canMove)
        {
            startPoint = transform.position;
            target = otherPoints[0].position;

        }

    }


    public override void uniqueUpdate()
    {
        if (canMove)
        {
            if (Vector3.Distance(transform.position, target) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime *2);
            }
            else
            {
                if (pointPos + 1 >= otherPoints.Length)
                {
                    target = startPoint;
                    pointPos = -1;

                }
                else
                {
                    pointPos += 1;

                    target = otherPoints[pointPos].position;
                }
            }
        }
    }

}
