using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Transform[] points;
    [SerializeField] private Vector3[] coursePostion;

    public Transform[] Points => points;
    public Vector3 EntityPosition { get; set; }


    private void Start()
    {
        EntityPosition = transform.position;
        coursePostion = new Vector3[points.Length];
        SetCoursePosition();
    }

    public Vector3 GetPosition(int pointIndex)
    {
        return coursePostion[pointIndex];
    }

    private void SetCoursePosition()
    {
        for (int i = 0; i < coursePostion.Length; i++)
        {
            coursePostion[i] = EntityPosition + points[i].localPosition;
        }
    }


}
