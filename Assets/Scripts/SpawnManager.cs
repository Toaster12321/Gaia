using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private Dictionary<string, Vector3> spawnLocations = new Dictionary <string, Vector3>();

    private void Start()
    {
        spawnLocations.Add("MainScene", new Vector3(-0.37f, 2.701f, -17.49f));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnLocations.TryGetValue(scene.name, out Vector3 spawnPosition))
        {
            FirstPersonController.instance.transform.position = spawnPosition;
            Physics.SyncTransforms();
        }
    }
}
