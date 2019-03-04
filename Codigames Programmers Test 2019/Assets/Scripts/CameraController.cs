using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 m_touchPosition;
    private Vector3 m_currentPosition;
    private Vector3 m_dragStartCameraPosition;
    private float m_cameraY;

    private void Start()
    {
        m_cameraY = transform.position.y;
    }

    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
#if UNITY_EDITOR
            m_touchPosition = Input.mousePosition;
#elif (UNITY_ANDROID || UNITY_IPHONE)
            m_touchPosition = Input.GetTouch(0).position;
#endif
            m_dragStartCameraPosition = transform.position;
        }

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
        {
#if UNITY_EDITOR
            m_currentPosition = Input.mousePosition;
#elif (UNITY_ANDROID || UNITY_IPHONE)
            m_currentPosition = Input.GetTouch(0).position;
#endif
            InputDrag();
        }
    }

    private void InputDrag()
    {
        m_currentPosition.z = m_touchPosition.z = m_dragStartCameraPosition.y;

        Vector3 direction = Camera.main.ScreenToWorldPoint(m_currentPosition) - Camera.main.ScreenToWorldPoint(m_touchPosition);
        direction *= -1;

        Vector3 position = m_dragStartCameraPosition + direction;
        transform.position = new Vector3(position.x, m_cameraY, position.z);
    }
}