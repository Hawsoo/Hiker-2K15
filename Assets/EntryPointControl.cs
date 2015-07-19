using UnityEngine;
using System.Collections;

public class EntryPointControl : MonoBehaviour
{
    public GameObject player;

    public static int entryPointID = 0;

    public Transform entryPoint0;
    public Transform entryPoint1;
    public Transform entryPoint2;
    public Transform entryPoint3;

    // Set player to entrypoint
    void Awake()
    {
        if (entryPointID == 0) player.transform.position = entryPoint0.position;
        else if (entryPointID == 1) player.transform.position = entryPoint1.position;
        else if (entryPointID == 2) player.transform.position = entryPoint2.position;
        else if (entryPointID == 3) player.transform.position = entryPoint3.position;
        //else 
    }

    // Change scenes
    public static void ChangeScenes(int entryPointID, string sceneName)
    {
        // Load level at correct entry point
        EntryPointControl.entryPointID = entryPointID;
        Application.LoadLevel(sceneName);
    }

    public static void ChangeScenes(string sceneName)
    {
        // Load level
        Application.LoadLevel(sceneName);
    }
}
