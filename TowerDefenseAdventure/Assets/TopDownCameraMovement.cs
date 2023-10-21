using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject targetToFollow;
    [SerializeField]
    Vector3 cameraOffset;
    void Start()
    {
        transform.position = targetToFollow.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetToFollow.transform.position + cameraOffset;
    }
}
