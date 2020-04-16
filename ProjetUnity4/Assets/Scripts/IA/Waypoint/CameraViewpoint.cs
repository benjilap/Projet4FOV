using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewpoint : MonoBehaviour {

    [SerializeField]
    protected float debugDrawRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(debugDrawRadius, debugDrawRadius, debugDrawRadius));
    }
}
