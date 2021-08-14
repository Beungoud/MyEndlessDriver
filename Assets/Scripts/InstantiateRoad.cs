using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class InstantiateRoad : MonoBehaviour
{

    public GameObject streetBlock;
    //    public GameObject[] decoList;
    private LevelManager levelManager;

    private List<GameObject> instantiatedBlocks = new List<GameObject>();
    private List<GameObject> objectPool = new List<GameObject>();
    private GameObject root;

    private float renderedMin = 0;
    private float renderedMax = 0;
    private float tilesLenght;

    [SerializeField]
    Transform roadDestructionLimit;

    // Use this for initialization
    void Start()
    {
        levelManager = GameManager.instance.LevelManager;            
        GameManager.instance.stepBack += stepBack;
        // Initialize the main road block information
        root = new GameObject();
        root.name = "Street";
        Renderer rend = streetBlock.GetComponentInChildren<Renderer>();
        tilesLenght = rend.bounds.size.z;

        Vector3 newPosition = new Vector3(0, 0, renderedMax);

        // Load all available resources
     //   decoList = Resources.LoadAll<GameObject>("Deco");

        // Create the first tile .
        createNewTile(newPosition);
    }

    // Update is called once per frame
    void Update()
    {
        while (transform.position.z > renderedMax)
        {
            Vector3 newPosition = new Vector3(0, 0, renderedMax + tilesLenght);
            createNewTile(newPosition);
        }
        while (roadDestructionLimit.position.z - 20f > renderedMin)
        {
            removeTile(renderedMin);
        }

    }

    public void FixedUpdate()
    {
        centerAll();
    }

    private void stepBack(float distance)
    {
        //UnityEngine.GameObject.FindGameObjectsWithTag
        //Transform rootTransform = transform.root;
        for (int i = 0; i < root.transform.childCount; i++)
        {
            Transform trans = root.transform.GetChild(i);
            trans.position = trans.position + Vector3.back * distance;
        }
        transform.parent.position += Vector3.back * distance;

        renderedMax -= distance;
        renderedMin -= distance;

    }

    private void centerAll()
    {
        if (renderedMin >= 10)
        {
            GameManager.instance.StepBack(10);
        }
    }

    private void removeTile(float toRemoveOffset)
    {
        foreach (GameObject roadBlock in instantiatedBlocks)
        {
            if (roadBlock.transform.position.z == toRemoveOffset)
            {
                renderedMin = roadBlock.transform.position.z + tilesLenght;
                instantiatedBlocks.Remove(roadBlock);
                Transform roadParent = roadBlock.transform.parent;
                roadBlock.transform.parent = root.transform;
                objectPool.Add(roadBlock);
                roadBlock.SetActive(false);
                Destroy(roadParent.gameObject);
                break;
            }
        }
    }

    private void createNewTile(Vector3 newPosition)
    {
        GameObject roadBlock;
        GameObject parentGo = new GameObject();
        parentGo.name = "Parent";
        if (objectPool.Count > 0)
        {
            roadBlock = objectPool[0];
            objectPool.RemoveAt(0);
            roadBlock.SetActive(true);
        }
        else {
            roadBlock = Instantiate<GameObject>(streetBlock);
        }
        roadBlock.transform.parent = parentGo.transform;
        roadBlock.transform.position = Vector3.zero;

        parentGo.transform.parent = root.transform;
        parentGo.transform.position = newPosition;

//        int value = UnityEngine.Random.Range(0, decoList.Length);
        int value = UnityEngine.Random.Range(0, levelManager.CurrentLevelDefinition.decoList.Length);


        //GameObject Deco1 = Instantiate<GameObject>(decoList[value]);
        GameObject Deco1 = Instantiate<GameObject>(levelManager.CurrentLevelDefinition.decoList[value]);
        Deco1.transform.parent = parentGo.transform;

        Deco1.transform.localPosition = new Vector3(10, 0, 0);
        Deco1.transform.Rotate(Vector3.up, -90);

        //value = UnityEngine.Random.Range(0, decoList.Length);
        //GameObject Deco2 = Instantiate<GameObject>(decoList[value]);
        value = UnityEngine.Random.Range(0, levelManager.CurrentLevelDefinition.decoList.Length);
        GameObject Deco2 = Instantiate<GameObject>(levelManager.CurrentLevelDefinition.decoList[value]);
        Deco2.transform.parent = parentGo.transform;

        Deco2.transform.localPosition = new Vector3(-10, 0, 0);
        Deco2.transform.Rotate(Vector3.up, 90);

        instantiatedBlocks.Add(roadBlock);
        renderedMax = newPosition.z;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, (renderedMax - renderedMin) / 2 + renderedMin), new Vector3(10, 10, renderedMax - renderedMin));
    }
}
