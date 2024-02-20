using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light dirLight;
    private ParticleSystem _ps;
    private bool isRain = false;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRain && dirLight.intensity > 0.3f)
        {
            lightIntensity(-1);
        }
        else if (!isRain && dirLight.intensity < 0.5f)
        {
            lightIntensity(1);
        }

    }

    private void lightIntensity(int mult)
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f));
            if (isRain)
                _ps.Stop();
            else
                _ps.Play();
            isRain = !isRain;
        }
    }
}
