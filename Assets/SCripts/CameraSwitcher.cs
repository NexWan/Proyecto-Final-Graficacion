using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // An array to hold all the cameras to switch between
    public float switchInterval = 5f; // Time interval between camera switches
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Activate the first camera in the array
        ActivateCurrentCamera();

        // Start the coroutine to switch between cameras
        StartCoroutine(SwitchCamera());
    }

    private IEnumerator SwitchCamera()
    {
        while (true)
        {
            // Move to the next camera in the array
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Deactivate the current camera
            DeactivateCurrentCamera();

            // Activate the new camera
            ActivateCurrentCamera();

            yield return new WaitForSeconds(switchInterval);
        }
    }

    private void DeactivateCurrentCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
    }

    private void ActivateCurrentCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}