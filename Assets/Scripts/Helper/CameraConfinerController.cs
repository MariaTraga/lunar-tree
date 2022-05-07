using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerController : MonoBehaviour
{
    [SerializeField] CinemachineConfiner cinemachineConfiner;

    private void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        GameObject cameraConfiner = FindObjectOfType<CameraConfiner>().gameObject;
        if (cameraConfiner == null) 
        {
            cinemachineConfiner.m_BoundingShape2D = null;
            return; 
        }

        Collider2D bounds = cameraConfiner.GetComponent<Collider2D>();
        cinemachineConfiner.m_BoundingShape2D = bounds;
    }
}
