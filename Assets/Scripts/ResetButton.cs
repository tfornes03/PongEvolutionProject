using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private bool alreadyPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyPressed)
        {
            alreadyPressed = true;
            Debug.Log("Reset Button Triggered by: " + other.name);
            FindObjectOfType<GameManager>()?.RestartGame();
        }
    }

    public void ResetButtonState()
    {
        alreadyPressed = false;
    }
}
