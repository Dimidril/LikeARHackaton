using UnityEngine;

namespace Plugins.FreeFlyCamera.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class FreeFlyCamera : MonoBehaviour
    {
        [Space] [SerializeField] [Tooltip("The script is currently active")]
        bool _active = true;

        [Space] [SerializeField] [Tooltip("Camera rotation by mouse movement is active")]
        bool _enableRotation = true;

        [SerializeField] [Tooltip("Sensitivity of mouse rotation")]
        float _mouseSense = 1.8f;

        [Space] [SerializeField] [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
        bool _enableTranslation = true;

        [SerializeField] [Tooltip("Velocity of camera zooming in/out")]
        float _translationSpeed = 55f;

        [Space] [SerializeField] [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
        bool _enableMovement = true;

        [SerializeField] [Tooltip("Camera movement speed")]
        float _movementSpeed = 10f;

        [SerializeField] [Tooltip("Speed of the quick camera movement when holding the 'Left Shift' key")]
        float _boostedSpeed = 50f;

        [SerializeField] [Tooltip("Boost speed")]
        KeyCode _boostSpeed = KeyCode.LeftShift;

        [SerializeField] [Tooltip("Move up")] KeyCode _moveUp = KeyCode.E;

        [SerializeField] [Tooltip("Move down")]
        KeyCode _moveDown = KeyCode.Q;

        [Space] [SerializeField] [Tooltip("Acceleration at camera movement is active")]
        bool _enableSpeedAcceleration = true;

        [SerializeField] [Tooltip("Rate which is applied during camera movement")]
        float _speedAccelerationFactor = 1.5f;

        [Space] [SerializeField] [Tooltip("This keypress will move the camera to initialization position")]
        KeyCode _initPositonButton = KeyCode.R;

        CursorLockMode _wantedMode;

        float _currentIncrease = 1;
        float _currentIncreaseMem = 0;

        Vector3 _initPosition;
        Vector3 _initRotation;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (_boostedSpeed < _movementSpeed)
                _boostedSpeed = _movementSpeed;
        }
#endif


        void Start()
        {
            var t = transform;

            _initPosition = t.position;
            _initRotation = t.eulerAngles;
        }

        void OnEnable()
        {
            if (_active)
                _wantedMode = CursorLockMode.Locked;
        }

        // Apply requested cursor state
        void SetCursorState()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = _wantedMode = CursorLockMode.None;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _wantedMode = CursorLockMode.Locked;
            }

            // Apply cursor state
            Cursor.lockState = _wantedMode;
            // Hide cursor when locking
            Cursor.visible = (CursorLockMode.Locked != _wantedMode);
        }

        void CalculateCurrentIncrease(bool moving)
        {
            _currentIncrease = Time.deltaTime;

            if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
            {
                _currentIncreaseMem = 0;
                return;
            }

            _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
            _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
        }

        void Update()
        {
            if (!_active)
                return;

            SetCursorState();

            if (Cursor.visible)
                return;

            // Translation
            if (_enableTranslation)
            {
                transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * Time.deltaTime * _translationSpeed);
            }

            // Movement
            if (_enableMovement)
            {
                var deltaPosition = Vector3.zero;
                var currentSpeed = _movementSpeed;

                if (Input.GetKey(_boostSpeed))
                    currentSpeed = _boostedSpeed;

                if (Input.GetKey(KeyCode.W))
                    deltaPosition += transform.forward;

                if (Input.GetKey(KeyCode.S))
                    deltaPosition -= transform.forward;

                if (Input.GetKey(KeyCode.A))
                    deltaPosition -= transform.right;

                if (Input.GetKey(KeyCode.D))
                    deltaPosition += transform.right;

                if (Input.GetKey(_moveUp))
                    deltaPosition += transform.up;

                if (Input.GetKey(_moveDown))
                    deltaPosition -= transform.up;

                // Calc acceleration
                CalculateCurrentIncrease(deltaPosition != Vector3.zero);

                transform.position += deltaPosition * currentSpeed * _currentIncrease;
            }

            // Rotation
            if (_enableRotation)
            {
                // Pitch
                Transform t;
                (t = transform).rotation *= Quaternion.AngleAxis(
                    -Input.GetAxis("Mouse Y") * _mouseSense,
                    Vector3.right
                );

                // Paw
                var eulerAngles = t.eulerAngles;
                transform.rotation = Quaternion.Euler(
                    eulerAngles.x,
                    eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense,
                    transform.eulerAngles.z
                );
            }

            // Return to init position
            if (Input.GetKeyDown(_initPositonButton))
            {
                var t = transform;
                
                t.position = _initPosition;
                t.eulerAngles = _initRotation;
            }
        }
    }
}