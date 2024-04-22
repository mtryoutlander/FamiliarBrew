using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnTimer;

    //respawn the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other);
        }
    }

    private IEnumerator RespawnPlayer(Collider other)
    {
        Debug.Log("player Being Resapwned");
        yield return new WaitForSeconds(spawnTimer);

        other.transform.position = spawnPoint.position;
    }
}
