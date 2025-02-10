using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public bool cameraLocked; 
    public float panSpeed;
    private GameObject player;
    private const float maxZoom = 10f, minZoom = 3f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown("y"))
        {
            cameraLocked = !cameraLocked;
        }

        // mouse is at right edge of screen
        if (Input.mousePosition.x >= Screen.width * 0.95)
        {
            transform.position += new Vector3(panSpeed * Time.deltaTime, 0, 0);
        }

        // left edge
        if (Input.mousePosition.x <= Screen.width * 0.05)
        {
            transform.position -= new Vector3(panSpeed * Time.deltaTime, 0, 0);
        }

        // top edge
        if (Input.mousePosition.y >= Screen.height * 0.95)
        {
            transform.position += new Vector3(0, panSpeed * Time.deltaTime, 0);
        }

        // bottom edge
        if (Input.mousePosition.y <= Screen.height * 0.05)
        {
            transform.position -= new Vector3(0, panSpeed * Time.deltaTime, 0);
        }

        // zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > minZoom)
            {
                Camera.main.orthographicSize -= 1;
            }
        }

        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize < maxZoom)
            {
                Camera.main.orthographicSize += 1;
            }
        }

        // center camera on player
        if (Input.GetButtonDown("Jump") || cameraLocked)
        {
            CenterCamera();
        }
    }

    void CenterCamera()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
