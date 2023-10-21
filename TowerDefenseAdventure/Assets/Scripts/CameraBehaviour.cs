using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public bool _fpsToTopDown = false;
    public bool _topDownToFps = false;
    private Transform _targetTransform;
    private Camera _camera;
    
    [Header("Camera settings")]
    [SerializeField] private float _topDownFOV = 65f;
    [SerializeField] private float _FpsFOV = 80f;
    [SerializeField] private Vector3 _relativeTopDownPos = new(0,20,0);
    [SerializeField] private float _fpsHeight = 0.5f;
    [SerializeField] private float _cameraTransitionSpeed = 10;
    [SerializeField] private Quaternion _topDownRotation = Quaternion.Euler(65,0,0);
    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _lookXLimit = 90.0f;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        if(_fpsToTopDown)
        {
            if(MoveFpsToTopDown(_cameraTransitionSpeed))
                _fpsToTopDown = false;
                
        }
        else if(_topDownToFps)
        {
            if(MoveTopDownTofps(_cameraTransitionSpeed))
                _topDownToFps = false;
        }
    }
     public void UpdateTargetTransform(Transform targetTransform)//à appeler dans update de la target
    {
        _targetTransform = targetTransform;
    }

    //Caméra follow types
    public void FollowFps()//call depuis le playerController
    {
        transform.position = _targetTransform.position + new Vector3(0,_fpsHeight);
    }
    public void FollowTopDown()
    {
        transform.position = _targetTransform.position + _relativeTopDownPos;
    }

    //Transition de Caméra
    private bool MoveFpsToTopDown(float speed)
    {
        _camera.fieldOfView = Mathf.Lerp(_FpsFOV, _topDownFOV, Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position,_targetTransform.position+_relativeTopDownPos, Time.deltaTime *speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,_topDownRotation, Time.deltaTime * speed);
        Vector3 distVect = transform.position - (_targetTransform.position+_relativeTopDownPos);
        return distVect.magnitude < .01f;
    }
    private bool MoveTopDownTofps(float speed)
    {
        _camera.fieldOfView = Mathf.Lerp(_topDownFOV, _FpsFOV, Time.deltaTime);
        Vector3 targetPoint = _targetTransform.position + new Vector3(0,_fpsHeight,0);
        transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetTransform.rotation, Time.deltaTime *speed);
        Vector3 distVect = transform.position - targetPoint;
        return distVect.magnitude < .01f;
    }
}
