using UnityEngine;
using System.Collections;

[System.Serializable]
public class CutsceneControls_AssociatedEntities
{
    // Contains instances involved in the cutscene
    public GameObject bear;
    public GameObject player;
    public GameObject camera;
}

public class CutsceneControls : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCam;
    public GameObject playerCamInner;
    public GameObject bearChaser;

    public CutsceneControls_AssociatedEntities cutsceneEntities;

    // Starts cutscene
    void StartCutscene(Collider other)
    {
        // Disable player and cam for cutscene
        player.SetActive(false);
        playerCam.SetActive(false);

        // Activate cutscene
        cutsceneEntities.bear.SetActive(true);
        cutsceneEntities.player.SetActive(true);
        cutsceneEntities.camera.SetActive(true);

        GetComponent<Animator>().enabled = true;
    }

    // Ends cutscene
    void EndCutsene()
    {
        // Set player and cam to correct place
        player.transform.position = playerCam.transform.position = cutsceneEntities.player.transform.position;
        playerCamInner.transform.rotation = cutsceneEntities.camera.transform.rotation;
        player.GetComponent<PlayerMovement>().ResetVelocity();

        // Setup bear chaser
        bearChaser.transform.position = cutsceneEntities.bear.transform.position;

        // Deactivate cutscene
        GetComponent<Animator>().enabled = false;

        cutsceneEntities.bear.SetActive(false);
        cutsceneEntities.player.SetActive(false);
        cutsceneEntities.camera.SetActive(false);

        // Enable player and cam
        player.SetActive(true);
        playerCam.SetActive(true);

        // Activate bear chaser
        bearChaser.SetActive(true);
    }
}
