using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    [SerializeField] Vector3 startCoord;
    [SerializeField] float endByX;
    [SerializeField] Vector3 direction;
    float speed = 3f;
    private void Update() {
       
        if(gameObject.transform.position.x < -Mathf.Abs(endByX)) {
            StartCoroutine(StopAndWait());
        }
        transform.Translate(speed * direction * Time.deltaTime);
    }
    public IEnumerator StopAndWait() {
        speed = 0;
        gameObject.transform.position = startCoord;
        yield return new WaitForSeconds(Random.Range(5, 40));
        speed = 3f;
    }
}
