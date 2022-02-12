using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Material activeMat;
    public Material inactiveMat;
    public GameObject interactCursor;

    private GameManager gameManager;
    private GameObject player;

    private void Start()
    {
        interactCursor.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player = GameObject.Find("Player");
    }

    private void OnMouseOver()
    {
        this.GetComponent<MeshRenderer>().material = activeMat;
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 2.5f)
        {
            interactCursor.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<AudioSource>().Play(0);
                interactCursor.SetActive(false);
                gameManager.objCounter++;
            }
        }
    }
    private void OnMouseExit()
    {
        this.GetComponent<MeshRenderer>().material = inactiveMat;
        interactCursor.SetActive(false);
    }
}
