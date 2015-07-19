using UnityEngine;
using System.Collections;

public class GameCheckpoint : MonoBehaviour
{
    public int checkpointID = 1;

    // Sets the checkpoint
    void OverrideCheckpoint()
    {
        EntryPointControl.entryPointID = checkpointID;
    }
}
