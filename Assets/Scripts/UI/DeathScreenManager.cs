
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private Button _respawnButton;
    private void OnEnable() {
        _respawnButton.onClick.AddListener(Respawn);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Respawn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDisable() {
        _respawnButton.onClick.RemoveListener(Respawn);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
