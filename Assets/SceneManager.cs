using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    public string sceneName;

    // Messages
    void GotoScene(Collider other)
    {
        // Go to scene with next index
        EntryPointControl.ChangeScenes(sceneName);
    }
}
