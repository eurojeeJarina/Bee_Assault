using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab")][SerializeField] GameObject deathFx;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            StartDeathSequence();
            deathFx.SetActive(true);
            Invoke("ReloadLevel", levelLoadDelay);
        }
 
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Particles Collided " + other.name);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        
    }
    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
