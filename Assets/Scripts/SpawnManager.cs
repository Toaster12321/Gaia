using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private Dictionary<string, Vector3> spawnLocations = new Dictionary <string, Vector3>();

    const string MAIN_SCENE = "MainScene";

    public static SpawnManager instance;

    private void Awake()
    {
        GameManager.Instance.SpawnManager = this;

        if (instance == null)
            instance = this;
    }

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
            print("spawning player");
            if (scene.name == MAIN_SCENE)
            {
                FirstPersonController.instance.HidePlayer(true);
            }

            Physics.SyncTransforms();
        }
    }


    public void Save(ref SceneSaveData data) //passes in reference, not copy, to read and write data
    {
        data.sceneName = SceneManager.GetActiveScene().name;
    }


    public void Load(SceneSaveData data)
    {
        SceneManager.LoadSceneAsync(data.sceneName).completed += OnSceneLoaded;
    }


    public void OnSceneLoaded(AsyncOperation asyncOp)
    {
        SaveManager.HandleLoadData();
    }

}

[System.Serializable]
public struct SceneSaveData //struct makes a default value for position to obtain
{
    public string sceneName;
}