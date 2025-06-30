using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuExit : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float creditsTime;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject endingUI;
    [SerializeField] private GameObject[] noButtons;
    private List<bool> completedQuests;
    private GameObject player;

    void Start()
    {
        endingUI.SetActive(false);
        exitMenu.SetActive(false);
        finishMenu.SetActive(false);
        credits.SetActive(false);

        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Movement>().StopSound();

        endingUI.SetActive(true);

        if (completedQuests.TrueForAll(AllQuestComplete) && completedQuests.Count > 0)
        {
            finishMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(noButtons[1]);
        }
        else
        {
            exitMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(noButtons[0]);
        }
    }

    public void LoadData(GameData data)
    {
        completedQuests = data.finishedQuests;
    }

    public void SaveData(ref GameData data)
    {

    }
    public void CancelExit()
    {
        exitMenu.SetActive(false);
        finishMenu.SetActive(false);
        endingUI.SetActive(false);

        var movement = player.GetComponent<Movement>();
        if (movement != null)
            movement.enabled = true;
    }

    private static bool AllQuestComplete(bool list)
    {
        return list = true;
    }

    public void ToStartMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void ToFinishTheGame()
    {
        credits.SetActive(true);
        StartCoroutine(DelayFinish());
    }

    IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(creditsTime);
        ToStartMenu();
    }
}
