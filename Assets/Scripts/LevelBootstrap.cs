using System.Collections;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        if (SaveManager.HasSave())
            SaveManager.Load();
    }
}
