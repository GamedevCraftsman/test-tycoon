using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private Vector2 minLimits;
    [SerializeField] private Vector2 maxLimits;

    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _lastTouchPosition;
    private Coroutine _dragCoroutine;
    private bool _isDragging;

    public void Initialize()
    {
        _targetPosition = transform.position;
        StartCheckDrag();
    }

    private IEnumerator CheckDrag()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (true)
        {
            MoveView();
            yield return waitForEndOfFrame;
            MoveCamera();
            yield return null;
        }
    }

    private void MoveView()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#endif
        HandleTouchInput();
    }

    private void MoveCamera()
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, 0.2f);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    // Mobile
    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _lastTouchPosition = touch.position;
                _isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                Vector3 touchDelta = (touch.position - (Vector2)_lastTouchPosition);

                MoveCamera(new Vector3(touchDelta.x, 0, touchDelta.y));

                _lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _isDragging = false;
            }
        }
    }

    // Unity Editor/PC
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastTouchPosition = Input.mousePosition;
            _isDragging = true;
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - _lastTouchPosition;
            MoveCamera(new Vector3(mouseDelta.x, 0, mouseDelta.y));
            _lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    void MoveCamera(Vector3 direction)
    {
        _targetPosition = transform.position;

        _targetPosition.x += direction.x * panSpeed * Time.deltaTime;
        _targetPosition.z += direction.z * panSpeed * Time.deltaTime;

        _targetPosition.x = Mathf.Clamp(_targetPosition.x, minLimits.x, maxLimits.x);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, minLimits.y, maxLimits.y);
    }

    public void StopCheckDrag()
    {
        StopCoroutine(_dragCoroutine);
    }

    public void StartCheckDrag()
    {
        _dragCoroutine = StartCoroutine(CheckDrag());
    }
}