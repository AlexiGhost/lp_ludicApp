using jumpAndLearn.terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SavePlayer();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    //Teleport the player on the last platform he touched
    private void SavePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 position;
        if (Platform.CurrentPlatform != null)
            position = Platform.CurrentPlatform.transform.position;
        else
            position = new Vector3(0f, 1f, -4.5f);
        position.y = 1;
        Quaternion rotation = new Quaternion();
        player.transform.SetPositionAndRotation(position, rotation);
    }
}
