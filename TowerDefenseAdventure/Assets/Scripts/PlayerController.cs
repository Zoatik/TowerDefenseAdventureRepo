using UnityEngine;
using UnityEngine.AI;
using Cinemachine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraPrefab;
    [SerializeField]
    private float _jumpSpeed = 8.0f;
    [SerializeField]
    private float _lookSpeed = 2.0f;
    [SerializeField]
    private float _lookXLimit = 90.0f;
    [SerializeField]
    private float _gravity = 10.0f;

    private CameraBehaviour _cameraBehaviour;
    private Camera _playerCamera;
    private TopDownCombat _topDownCombat;

    private PlayerInfos _playerInfos;
    private Enemy _target = null;

    private bool _canMove = true;
    private bool _fpsMode = true;

    NavMeshAgent agent;
    CharacterController characterController;
    InputManager inputManager;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    private float rotationY = 0;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        inputManager = GetComponent<InputManager>();
        _playerInfos = GetComponent<Player>().GetPlayerInfos();
        _playerCamera = Instantiate(_cameraPrefab);
        _cameraBehaviour = _playerCamera.GetComponent<CameraBehaviour>();
        _topDownCombat = GetComponent<TopDownCombat>();
    }
    void Start()
    {
        agent.speed = _playerInfos.playerData.runSpeed;
        Debug.Log(agent.speed);
        // Lock cursor
        if(_fpsMode)
        {
            _playerCamera.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _cameraBehaviour.UpdateTargetTransform(transform);
        if(inputManager.GetKeyDown(KeyBindingActions.ChangeGameMode))
        {
            SwitchGameMode();
        }
        if(_fpsMode)
        {
            _canMove = !_cameraBehaviour._topDownToFps;
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = _canMove ? (isRunning ? _playerInfos.playerData.runSpeed : _playerInfos.playerData.walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = _canMove ? (isRunning ? _playerInfos.playerData.runSpeed : _playerInfos.playerData.walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (inputManager.GetKey(KeyBindingActions.FPSJump) && _canMove && characterController.isGrounded)
            {
                moveDirection.y = _jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= _gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (!_cameraBehaviour._topDownToFps)
            {
                float mouseX = Input.GetAxis("Mouse X")*_lookSpeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y")*_lookSpeed * Time.deltaTime;
                rotationY = transform.rotation.eulerAngles.y;
                rotationY += mouseX;
                rotationX -= mouseY;
                rotationX = Mathf.Clamp(rotationX, -_lookXLimit, _lookXLimit);

                _playerCamera.transform.rotation = Quaternion.Euler(rotationX,rotationY,0f);
                transform.Rotate(Vector3.up * mouseX);
                _cameraBehaviour.FollowFps();
            }
        }
        else
        {
            //TOPDOWN
            if (!_cameraBehaviour._fpsToTopDown)
            {
                
                _cameraBehaviour.FollowTopDown();
                if(_target != null)
                {
                    agent.destination = _target.transform.position;

                }
                if(inputManager.GetKey(KeyBindingActions.TopDownMove))
                {
                    Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        _target = null;
                        if (hit.collider.CompareTag("Floor"))
                        {
                            agent.stoppingDistance = 0;
                            agent.destination = hit.point;
                        }
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            PlayerSpells.PlayerSpellData spellData = _playerInfos.possessedSpells.playerSpellData[(int)TopDownCombat.AttackSpells.Basic];
                            agent.stoppingDistance = spellData.baseRange;
                            
                            _topDownCombat.target = hit.collider.GetComponent<Enemy>();
                            _target = _topDownCombat.target;
                            _topDownCombat.Attack(TopDownCombat.AttackSpells.Basic);
                        }
                    }
                }
                if(inputManager.GetKey(KeyBindingActions.TopDownSelect))
                {
                    Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.CompareTag("Floor"))
                        {
                            
                        }
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            _topDownCombat.target = hit.collider.GetComponent<Enemy>();
                        }
                    }
                }
            }
        }
    }

    public void SwitchGameMode()
    {
        _fpsMode = !_fpsMode;
        Cursor.visible = !_fpsMode;
        agent.ResetPath();

        if(_fpsMode)
        {
            rotationX = 0;
            _cameraBehaviour._topDownToFps = true;
            _cameraBehaviour._fpsToTopDown = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _cameraBehaviour._fpsToTopDown = true;
            _cameraBehaviour._topDownToFps = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    

    public void SetFpsMode(bool isFpsMode) {_fpsMode = isFpsMode;}
    public bool GetFpsMode(){return _fpsMode;}
    public void SetCanMove(bool canMove) {_canMove = canMove;}
    public bool GetCanMove(){return _canMove;}
}
