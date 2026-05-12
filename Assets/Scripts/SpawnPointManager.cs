using UnityEngine;
using Unity.Netcode;

//Creating a network aware spawn manager script
public class SpawnPointManager : NetworkBehaviour 
{

    //stores which spawn point is to be used next
    //Static means all players will share thus value
    private static int nextSpawnIndex;

    public override void OnNetworkSpawn(){

        if (!IsServer)
        {
            return;
        }
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if(spawnPointObjects.Length == 0)
        {
            Debug.Log("No SpawnPoints Detetcted");
            return;
        }

        Transform selectedSpawnPoint = spawnPointObjects[nextSpawnIndex].transform;
        CharacterController characterContoller = GetComponent<CharacterController>();
        if (characterContoller != null)
        {
            characterContoller.enabled = false;
        }
        transform.position = selectedSpawnPoint.position;
        transform.rotation = selectedSpawnPoint.rotation;
        if (characterContoller != null)
        {
            characterContoller.enabled = true;
        }
        nextSpawnIndex++;
        if(nextSpawnIndex >= spawnPointObjects.Length)
        {
            nextSpawnIndex = 0;
        }
    }

    


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}