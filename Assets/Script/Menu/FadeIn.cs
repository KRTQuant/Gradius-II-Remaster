using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public AnimateLogo LogoGame;
    public GameObject LogoObj;
    private SpriteRenderer BgSprite;

    public float speedFade;

    // Start is called before the first frame update
    void Start()
    {
        LogoObj = GameObject.Find("LogoGame");
        LogoGame = LogoObj.GetComponent<AnimateLogo>();
        BgSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LogoGame.AnimateEnd == false)
        {
            
        }
        if(LogoGame.AnimateEnd == true)
        {
            float alphaVal = BgSprite.color.a;
            Color tmp = BgSprite.color;
            alphaVal += speedFade * Time.deltaTime;
            BgSprite.color = new Color(BgSprite.color.r, BgSprite.color.g, BgSprite.color.b, alphaVal);
            
            //StartCoroutine(FadeInSprite());
        }
        
    }

    private IEnumerator FadeInSprite()
    {
        float alphaVal = BgSprite.color.a;
        Color tmp = BgSprite.color;

        while (BgSprite.color.a <= 0)
        {
            alphaVal += 0.01f*Time.deltaTime;
            tmp.a = alphaVal;
            BgSprite.color = tmp;

            yield return new WaitForSeconds(0.01f); // update interval
        }
    }
}
