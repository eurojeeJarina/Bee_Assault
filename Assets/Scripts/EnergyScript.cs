using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();   
    }

    void OnTriggerEnter(Collider other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        
        GameObject fx = Instantiate(explosion,transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        Destroy(gameObject);
    }
}
