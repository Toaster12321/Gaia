using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CutsceneActivator : MonoBehaviour
{
    [SerializeField] private List<DialogueLines> dialogueLines;
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private bool playOnlyOnce = true;

    [Header ("Camera Settings")]
    [SerializeField] private bool cameraSwitches = false;
    [SerializeField] private Camera originalCamera;
    [SerializeField] private Camera newCamera;

    [Header("Object Hide Settings")]
    [SerializeField] private bool hideObjectAfter = false;
    [SerializeField] private GameObject objectToHide;

    [Header("Player Show Settings")]
    [SerializeField] private bool showPlayerAfter = false;



    private bool hasPlayedOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player == null)
            return;

        if (playOnlyOnce && hasPlayedOnce)
            return;


        if (cameraSwitches)
             SwitchCameras(true);

        if (cutsceneManager != null )
        {
            hasPlayedOnce = true;
            cutsceneManager.StartCutscene(dialogueLines);
        }
    }

    private void SwitchCameras(bool useNewCamera)
    {
        originalCamera.enabled = !useNewCamera;
        newCamera.enabled = useNewCamera;
    }

    private void OnCutsceneFinished()
    {
        SwitchCameras(false);
    }

    private void HideObject()
    {
        objectToHide.SetActive(false);
    }

    private void ShowPlayerAfter()
    {
        FirstPersonController.instance.HidePlayer(false);
    }

    private void OnEnable()
    {
        if (cameraSwitches)
        {
            CutsceneManager.cutsceneFinished += OnCutsceneFinished;
        }
        if (hideObjectAfter)
        {
            CutsceneManager.cutsceneFinished += HideObject;
        }
        if(showPlayerAfter)
        {
            CutsceneManager.cutsceneFinished += ShowPlayerAfter;
        }
    }

    private void OnDisable()
    {
        if (cameraSwitches)
        {
            CutsceneManager.cutsceneFinished -= OnCutsceneFinished;
        }
        if (hideObjectAfter)
        {
            CutsceneManager.cutsceneFinished -= HideObject;
        }
        if (showPlayerAfter)
        {
            CutsceneManager.cutsceneFinished -= ShowPlayerAfter;
        }
    }

}
