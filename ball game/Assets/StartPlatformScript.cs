using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatformScript : MonoBehaviour
{
    [SerializeField] private float startTimer = 0;
    [SerializeField] private float endTimer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        startTimer += 1f * Time.deltaTime;
        if (startTimer >= endTimer)
        {
            Destroy(gameObject);
        }
    }
    
}
