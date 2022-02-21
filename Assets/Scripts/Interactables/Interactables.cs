using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Material activeMat;
    public Material inactiveMat;

    private GameManager gameManager;
    private GameObject player;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player = GameObject.Find("Player");
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 2.5f)
        {
            this.GetComponent<MeshRenderer>().material = activeMat;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play(0);
                }
                gameManager.objCounter++;
            }
        }
    }
    private void OnMouseExit()
    {
        if (!player) return;
        this.GetComponent<MeshRenderer>().material = inactiveMat;
    }
}
