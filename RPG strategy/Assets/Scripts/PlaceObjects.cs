using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private float rotateSpeed = 60f;
    public LayerMask layer;
    void Start()
    {
        PositionObject();
    }
    private void PositionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
            transform.position = hit.point;
    }
    void Update()
    {
        PositionObject();
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<SpawnCarScript>().enabled = true;
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
