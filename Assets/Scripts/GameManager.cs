using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int objCounter;
    public Text objText;
    public Material glowyMat;
    public Material interactableMat;

    private int prevObjCounter;
    //private GameObject player;

    //Objective 1 (cane) objects
    public GameObject obj1Cane;
    public GameObject obj1BlockingWall;

    //Objective 2 (fridge) objects
    public GameObject obj2Fridge;

    //Objective 3 (toilet) objects
    public GameObject obj3Toilet;

    // Start is called before the first frame update
    void Start()
    {
        objCounter = 2;

        prevObjCounter = 0;

        //Disable interactability of future objectives
        obj3Toilet.GetComponent<Interactables>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(objCounter > prevObjCounter)
        {
            prevObjCounter = objCounter;
            StartCoroutine(Objectives());
        }

    }

    IEnumerator Objectives()
    {
        //Each Objective will disable previous obj's objects interactibility and enable new obj's objects 
        switch(objCounter)
        {
            case 1:
                {
                    objText.text = "Where did I put my cane?";
                    yield return new WaitForSeconds(5);
                    objText.gameObject.SetActive(false);

                    break;
                }
            case 2:
                {
                    obj1Cane.SetActive(false);
                    obj1BlockingWall.SetActive(false);

                    objText.gameObject.SetActive(true);
                    objText.text = "I'm thirsty, I could use a drink";
                    yield return new WaitForSeconds(5);
                    objText.gameObject.SetActive(false);

                    break;
                }
            case 3:
                {
                    Destroy(obj2Fridge.GetComponent<Interactables>());
                    obj2Fridge.GetComponent<MeshRenderer>().material = glowyMat;

                    //Wait until player finishes drinking water
                    yield return new WaitForSeconds(9f);

                    MeshRenderer[] toiletRenderers = obj3Toilet.GetComponentsInChildren<MeshRenderer>();
                    toiletRenderers[0].material = interactableMat;
                    toiletRenderers[1].material = interactableMat;
                    obj3Toilet.GetComponent<Interactables>().enabled = true;

                    objText.gameObject.SetActive(true);
                    objText.text = "I drank too much, I need to pee now";
                    yield return new WaitForSeconds(5);
                    objText.gameObject.SetActive(false);

                    break;
                }
        }
    }
}