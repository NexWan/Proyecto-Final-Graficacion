using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] text;
    // [SerializeField] private TextMeshProUGUI chickenUpdater;
    private GameObject panel;

    public bool switchScene;

    public bool tut = true;
    
    // [SerializeField] private float restingChicken;
    //
    // private float capturedChickens;

    public void changeGui()
    {
        if(!switchScene && tut)
            if (text[0].gameObject.activeSelf)
            {
                text[0].gameObject.SetActive(false);
                text[1].gameObject.SetActive(true);
            }
            else
            {
                panel = GameObject.FindWithTag("UIPanel");
                panel.SetActive(false);
                enableGame();
            }
        else if (switchScene)
        {
            GameManager.Instance.changeScene();
        }
    }

    public void enableGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            InputMovement playerMotion = player.GetComponent<InputMovement>();
            if (playerMotion != null) playerMotion.enabled = true;
        }

        UIInput ui = GetComponent<UIInput>();
        ui.enabled = false;
    }

    // public void updateChickens()
    // {
    //     chickenUpdater.text = $"Pollos capturados: {capturedChickens+= 1} \n" +
    //                           $"Pollos restantes: {restingChicken -= 1}";
    // }
}
