using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    /// <summary>
    /// Så att kameran följer efter bilen och vilken position kameran ska ha bakom bilen.
    /// </summary>
    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Så att kameran tittar åt det hållet bilen tittar åt, tex när den svänger.
    /// </summary>
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}