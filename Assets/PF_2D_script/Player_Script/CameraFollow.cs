using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]
    Vector3 offSet = new Vector3 (0,0,-10);//suis le joueur depuis la position : 0,0,-10 ( position par default)
    [SerializeField]
    public float smoothing = 0.25f;
    Vector3 velocity = Vector3.zero;
    [SerializeField]
    private Transform target;
        
    void Update()
    {
        Vector3 targetPosition = target.position + offSet;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
    }
}
