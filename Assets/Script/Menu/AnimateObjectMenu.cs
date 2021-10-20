using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimateObjectMenu : MonoBehaviour
{
    
    public AnimateLogo LogoGame;
    public GameObject LogoObj;

    public SpriteRenderer Konami;
    public GameObject KonamiObj;

    public Text KonamiTxt;
    public GameObject KonamiTextObj;

    public Text PlayTxt;
    public GameObject PlayTextObj;

    public Text OnePlayerTxt;
    public GameObject OnePlayerTextObj;

    public Text ExitTxt;
    public GameObject ExitTextObj;

    public Text TmTxt;
    public GameObject TmTextObj;

    public SpriteRenderer BackGroundSprite;
    public GameObject BackGroundObj;

    // Start is called before the first frame update
    void Start()
    {             
        LogoObj = GameObject.Find("LogoGame");
        LogoGame = LogoObj.GetComponent<AnimateLogo>();

        KonamiObj = GameObject.Find("Konami");
        Konami = KonamiObj.GetComponent<SpriteRenderer>();

        KonamiTextObj = GameObject.Find("KonamiText");
        KonamiTxt = KonamiTextObj.GetComponent<Text>();

        PlayTextObj = GameObject.Find("PlaySelectText");
        PlayTxt = PlayTextObj.GetComponent<Text>();

        OnePlayerTextObj = GameObject.Find("1PlayerText");
        OnePlayerTxt = OnePlayerTextObj.GetComponent<Text>();

        ExitTextObj = GameObject.Find("ExitText");
        ExitTxt = ExitTextObj.GetComponent<Text>();

        TmTextObj = GameObject.Find("TmText");
        TmTxt = TmTextObj.GetComponent<Text>();

        BackGroundObj = GameObject.Find("BGMenu");
        BackGroundSprite = BackGroundObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LogoGame.AnimateEnd == false)
        {
            Konami.enabled = false;
            KonamiTxt.enabled = false;
            PlayTxt.enabled = false;
            OnePlayerTxt.enabled = false;
            ExitTxt.enabled = false;
            TmTxt.enabled = false;
            BackGroundSprite.enabled = false;

        }
        if (LogoGame.AnimateEnd == true)
        {
            Konami.enabled = true;
            KonamiTxt.enabled = true;
            PlayTxt.enabled = true;
            OnePlayerTxt.enabled = true;
            ExitTxt.enabled = true;
            TmTxt.enabled = true;
            BackGroundSprite.enabled = true;
        }
    }
}
