using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Shooting shooting;
    [SerializeField] private GameObject ui;

    private void Start()
    {
        firstPersonController.enabled = false;
        shooting.enabled = false;
        ui.SetActive(true);
    }

    private void Update()
    {
        if (input.startGame)
        {
            ui.SetActive(false);

            shooting.enabled = true;
            firstPersonController.enabled = true;      
        }

        if (input.closeGame)
        {
            CloseGame();
        }
    }

    /// <summary>
    /// Private method of closing the game.
    /// </summary>
    private void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
