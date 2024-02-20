using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectControler : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, layerMask;
    private Camera cam;
    private GameObject cubeSelection;
    private RaycastHit hit;
    public List<GameObject> players;
    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && players.Count > 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
            {
                foreach (var el in players)
                    el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var el in players)
                if (el != null)
                    el.transform.GetChild(0).gameObject.SetActive(false);

            if (players.Count > 0)
                players.Clear();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, layer))
            {
                cubeSelection = Instantiate(cube, new Vector3(hit.point.x, 1, hit.point.z), Quaternion.identity);
            }
        }
        if (cubeSelection)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = (hit.point.x - hitDrag.point.x) * -1;
                float zScale = hit.point.z - hitDrag.point.z;
                if (xScale < 0.0f && zScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else if (xScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (zScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                cubeSelection.transform.localScale = new Vector3((Mathf.Abs(xScale)), 1, Mathf.Abs(zScale));
            }
        }
        if (Input.GetMouseButtonUp(0) && cubeSelection)
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                 cubeSelection.transform.GetChild(0).transform.position,
                 cubeSelection.transform.localScale / 2,
                 Vector3.down,
                 Quaternion.identity,
                 0,
                 layerMask
                 );
            foreach (var el in hits)
            {
                if (el.collider.CompareTag("Enemy")) continue;
                players.Add(el.transform.gameObject);
                el.transform.GetChild(0).gameObject.SetActive(true);
            }
            Destroy(cubeSelection);
        }
    }
}
