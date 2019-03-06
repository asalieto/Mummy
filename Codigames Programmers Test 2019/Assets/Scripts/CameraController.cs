using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float m_zoomSpeed = 0.3f;
    [SerializeField]
    private float m_dragSpeed = 1f;

    [Header("Camera bounds")]
    [SerializeField]
    private float m_boundsMinX = -15f;
    [SerializeField]
    private float m_boundsMaxX = 25f;
    [SerializeField]
    private float m_boundsMinY = 5;
    [SerializeField]
    private float m_boundsMaxY = 15f;
    [SerializeField]
    private float m_boundsMinZ = -45f;
    [SerializeField]
    private float m_boundsMaxZ = 10f;

    //BoxCollider as camera bounding box. Not very accesible for the designer
    //[SerializeField]
    //private BoxCollider m_cameraBounds;

    private Vector3 m_touchPosition;
    private Vector3 m_currentPosition;
    private Vector3 m_dragStartCameraPosition;
    
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.mouseScrollDelta.y != 0f)
        {
            Vector3 position = transform.position + (Input.mouseScrollDelta.y * m_zoomSpeed) * transform.forward;

            //if (m_cameraBounds.bounds.Contains(position))
            if (IsInsideBounds(position))
            {
                transform.position = position;
            }
        }
#else
#endif
        
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
    #if UNITY_EDITOR
            m_touchPosition = Input.mousePosition;
    #elif (UNITY_ANDROID || UNITY_IPHONE)
            m_touchPosition = Input.GetTouch(0).position;
    #endif
            m_dragStartCameraPosition = transform.position;
        }

        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
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

        Vector3 position = m_dragStartCameraPosition + (direction * m_dragSpeed);
        position = new Vector3(position.x, transform.position.y, position.z);

        //if (m_cameraBounds.bounds.Contains(position))
        if (IsInsideBounds(position))
        {
            transform.position = position;
        }
    }

    private bool IsInsideBounds(Vector3 position)
    {
        //TODO: maybe instead of deny the entire movement just apply the correct axis movement?
        return (position.x > m_boundsMinX && position.x < m_boundsMaxX &&
                position.y > m_boundsMinY && position.y < m_boundsMaxY &&
                position.z > m_boundsMinZ && position.z < m_boundsMaxZ);
    }
}