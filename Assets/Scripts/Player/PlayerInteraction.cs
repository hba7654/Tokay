using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //Materials
    public Material glowyMat;
    public Material itemMat;
    public Material monsterMat;
    public Material activeObjMat;
    public Material inactiveObjMat;
    public Material wallMat;

    //Will hold audio volume data
    private float[] spectrumData;

    //Speed at which color will diminish
    [SerializeField]
    private float diminishingSpeed;

    //Colors for each material
    private Color glowyColor;
    private Color itemColor;
    private Color monsterColor;
    private Color activeObjColor;
    private Color inactiveObjColor;
    private Color wallColor;

    //Interactables
    private GameObject playerCane;
    public GameObject enviroCane;
    //private GameObject playerShotgun;
    //private GameObject enviroShotgun;

    //Intensity of glow
    private float intensity;

    private AudioSource caneTap;


    // Start is called before the first frame update
    void Start()
    {
        glowyColor = Color.gray;
        itemColor = Color.green;
        monsterColor = Color.red;
        activeObjColor = Color.yellow;
        inactiveObjColor = Color.magenta;
        wallColor = Color.cyan;

        spectrumData = new float[128];

        playerCane = transform.GetChild(1).gameObject;
        playerCane.SetActive(false);

        intensity = 0;

        caneTap = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAudio();

        if(playerCane.activeInHierarchy && Input.GetMouseButtonDown(0) && !caneTap.isPlaying)
        {
            caneTap.Play();
            //intensity = 1; //override intensity bc it's too low :c
        }

       if(!enviroCane.activeInHierarchy && !playerCane.activeInHierarchy)
        {
            playerCane.SetActive(true);
            caneTap.Play();
        }
    }

    /// <summary>
    /// Checks audio values playing in-game and changes intensity accordingly
    /// </summary>
    void CheckAudio()
    {
        //Get audio data from listener
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        //If there is audio playing, begin changing colors
        if (spectrumData != null && spectrumData.Length > 0)
        {
            //If audio value is greater than intensity, change intensity
            //This is done to create diminishing glow rather than on/off
            intensity = (spectrumData[0] * 80 > intensity) ? spectrumData[0] * 80 : intensity;

            //Diminish glow of all mats
            DiminishGlow(glowyMat, glowyColor);
            DiminishGlow(itemMat, itemColor);
            DiminishGlow(monsterMat, monsterColor);
            DiminishGlow(activeObjMat, activeObjColor);
            DiminishGlow(inactiveObjMat, inactiveObjColor);
            DiminishGlow(wallMat, wallColor);
        }
    }

    /// <summary>
    /// Decrease the value of each material until it is black again
    /// </summary>
    /// <param name="mat">Material to alter</param>
    /// <param name="color">Material's color</param>
    void DiminishGlow(Material mat, Color color)
    {
        if (intensity <= 0.05f) intensity = 0;
        intensity -= Time.deltaTime * diminishingSpeed;

        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);

        v = intensity;

        Color newColor = Color.HSVToRGB(h, s, v);

        mat.SetColor("_Color", newColor);
    }
}
