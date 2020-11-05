using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField] Transform target;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 0.05f);
    }
}
