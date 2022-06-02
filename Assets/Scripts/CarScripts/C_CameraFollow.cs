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
    /// S� att kameran f�ljer efter bilen och vilken position kameran ska ha bakom bilen.
    /// </summary>
    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// S� att kameran tittar �t det h�llet bilen tittar �t, tex n�r den sv�nger.
    /// </summary>
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}