﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public intScriptableObject playerHealth;
    public intScriptableObject playerMAxHealth;
    public intScriptableObject playerGold;
    public SkillListScriptableObject BeginnerLoadoutSkill;
    public SkillListScriptableObject BeginnerLoadoutRanged;
    public SkillListScriptableObject equippedLoadoutSkill;
    public SkillListScriptableObject equippedLoadoutRanged;
    public GameObjectList playerList;
    public float count;
    public int currentLVL =0;
    public GameObject enemy;
    public GameObjectList navmeshObjects;
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

    private void Start()
    {

        RestartResources();


    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth.value <= 0)
        {
            GameOver();

        }
        //count += 7 * Time.deltaTime;
        //if (count > 100)
        //{
        //    Instantiate(enemy, transform.position, Quaternion.identity);
        //    count = 0;
        //}
    }

    public void GameOver()
    {
        ChangeScene(0);
        RestartResources();
        
    }

    public void RestartResources()
    {
        playerHealth.value = playerMAxHealth.value;
        playerGold.value = 100;
        equippedLoadoutRanged.list.Clear();
        equippedLoadoutSkill.list.Clear();
        equippedLoadoutRanged.list.Add(BeginnerLoadoutRanged.list[0]);
        equippedLoadoutSkill.list.Add(BeginnerLoadoutSkill.list[0]);
    }

    public void ChangeScene(int sceneIndex)
    {

        //SceneManager.LoadSceneAsync("WaveFunctionCollapse", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentLVL);
        StartCoroutine(LoadAsynchonously(sceneIndex));
        
        // playerList.list[0].gameObject.GetComponent<NavMeshAgent>().updatePosition = false;
         Invoke("InvokeNewScene", 0.4f);
       //playerList.list[0].gameObject.GetComponent<NavMeshAgent>().destination = new Vector3(10, 0.5f, 0); 
        playerList.list[0].gameObject.GetComponent<NavMeshAgent>().enabled = false;



    }

    public void InvokeNewScene()
    {
        playerList.list[0].gameObject.transform.position = new Vector3(10, 0.5f, 0);
        playerList.list[0].gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    IEnumerator LoadAsynchonously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        currentLVL = sceneIndex;
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
