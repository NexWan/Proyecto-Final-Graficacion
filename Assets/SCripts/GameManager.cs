using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject winPanel;
    [SerializeField] private TextMeshProUGUI chickenUpdater;
    [SerializeField] private float restingChicken;
    public string sceneName;
    public bool ENDGAME = false;

    private float capturedChickens;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Debug.Log("ola");
    }


    public void updateChickens()
    {
        chickenUpdater.text = $"Pollos capturados: {capturedChickens+= 1} \n" +
                              $"Pollos restantes: {restingChicken -= 1}";
        if (restingChicken == 0)
            endGame();
    }

    void endGame()
    {
        winPanel.gameObject.SetActive(true);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && !ENDGAME)
        {
            InputMovement playerMotion = player.GetComponent<InputMovement>();
            if (playerMotion != null) playerMotion.enabled = false;
        }
    }

    public void changeScene()
    {
        if(!ENDGAME)
            SceneManager.LoadScene(sceneName);
        else if (ENDGAME && restingChicken == 0)
            Application.Quit();
    }
}
