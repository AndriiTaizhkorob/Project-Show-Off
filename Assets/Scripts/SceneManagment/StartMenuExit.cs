using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class StartMenuExit : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float creditsTime;
    [SerializeField] private VisualEffect exitEffect;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject endingUI;
    [SerializeField] private GameObject[] noButtons;
    public List<bool> completedQuests;
    private GameObject player;

    void Awake()
    {
        exitEffect.Stop();
    }

    void Start()
    {
        endingUI.SetActive(false);
        exitMenu.SetActive(false);
        finishMenu.SetActive(false);
        credits.SetActive(false);

        player = GameObject.Find("Player");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Movement>().StopSound();
        player.GetComponent<FirePower>().StopSound();
        player.GetComponent<IcePower>().StopSound();

        if (AllQuestsComplete() && completedQuests.Count == 7)
        {
            endingUI.SetActive(true);
            finishMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(noButtons[1]);
        }
        else if(completedQuests.Count > 0)
        {
            endingUI.SetActive(true);
            exitMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(noButtons[0]);
        }
    }

    public void LoadData(GameData data)
    {
        completedQuests = data.finishedQuests;
        Debug.Log(completedQuests.Count);

        if (completedQuests.Count > 0)
        {
            exitEffect.Play();
        }
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

    private bool AllQuestsComplete()
    {
        int i = 0;

        foreach (bool quest in completedQuests)
        {
            if(quest)
                { i++; }
        }

        if (i >= 5)
            return true;
        else
            return false;
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
