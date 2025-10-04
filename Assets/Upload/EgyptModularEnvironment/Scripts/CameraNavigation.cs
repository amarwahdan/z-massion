using UnityEngine;

public class CameraNavigation : MonoBehaviour {

    public float rotSpeedH = 2.0f;
    public float rotSpeedV = 2.0f;
    public float moveSpeed = 5.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            moveSpeed += 3;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveSpeed -= 3;
        }

        yaw += rotSpeedH * Input.GetAxis("Mouse X") * Time.deltaTime;
        pitch -= rotSpeedV * Input.GetAxis("Mouse Y") * Time.deltaTime;
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(x, 0, z);
    }
}
