using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public float cameraSpeed = 10f, speed = 10f, zoomSpeed = 10f;
    private float speedMult = 1f;
    void Start()
    {

    }
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        float rotate = 0;
        if (Input.GetKey(KeyCode.Q))
            rotate = -1f;
        else if (Input.GetKey(KeyCode.E))
            rotate = 1f;

        speedMult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        transform.Rotate(Vector3.up * cameraSpeed * Time.deltaTime * rotate * speedMult, Space.World);
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * speedMult * speed, Space.Self);
        transform.position += transform.up * zoomSpeed * speed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 6f, 45f), transform.position.z);
    }
}
