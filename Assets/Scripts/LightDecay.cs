using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightDecay : MonoBehaviour
{
   
    public float decayRate = 0.01f; 
    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if (light2D.intensity > 0)
        {
            light2D.intensity -= decayRate * Time.deltaTime;

            if (light2D.intensity <= 0)
            {
                light2D.intensity = 0;
                SceneManager.LoadScene("DeathScene");
            }
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
}
