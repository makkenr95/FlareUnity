using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSequence : MonoBehaviour
{
    GameObject player;

    public TextMeshProUGUI title;
    public TextMeshProUGUI pressText;
    public SpriteRenderer pressTextSprite;
    public AnimationCurve textFadeCurve;
    public float textFadeSpeed;
    public float textFadeTimer;

    public IntroAudioHandler introAH;
    public AudioSource startGameAudioSource;

    public Image vignetteImage;
    public Image starVignetteImage;
    public AnimationCurve vignetteFadeCurve;
    public float vignetteFadeSpeed;
    public float vignetteFadeTimer;

    public bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {    

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //PAUSE MENU
            }
            else
            {
                startGameAudioSource.Play();
                fading = true;
                vignetteImage.color = new Color(vignetteImage.color.r, vignetteImage.color.g, vignetteImage.color.b, vignetteFadeCurve.Evaluate(0));
                starVignetteImage.color = new Color(vignetteImage.color.r, vignetteImage.color.g, vignetteImage.color.b, vignetteFadeCurve.Evaluate(0));
                introAH.PlayAudioClip(introAH.introStart);
            }
        }
        if (fading)
        {
            Fade();
        }
    }
    void Fade()
    {
        vignetteFadeTimer += Time.deltaTime * vignetteFadeSpeed;
        textFadeTimer += Time.deltaTime * textFadeSpeed;

        vignetteImage.color = new Color(vignetteImage.color.r, vignetteImage.color.g, vignetteImage.color.b, vignetteFadeCurve.Evaluate(vignetteFadeTimer));
        starVignetteImage.color = new Color(starVignetteImage.color.r, starVignetteImage.color.g, starVignetteImage.color.b, vignetteFadeCurve.Evaluate(vignetteFadeTimer));
        title.color = new Color(title.color.r, title.color.g, title.color.b, textFadeCurve.Evaluate(textFadeTimer));
        pressText.color = new Color(pressText.color.r, pressText.color.g, pressText.color.b, textFadeCurve.Evaluate(textFadeTimer));
        pressTextSprite.color = new Color(pressTextSprite.color.r, pressTextSprite.color.g, pressTextSprite.color.b, textFadeCurve.Evaluate(textFadeTimer));

        if (textFadeTimer >= 1 && vignetteFadeTimer >= 1)
        {
            fading = false;
            var playerStartBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStartBehavior>();
            playerStartBehavior.ActivatePlayer();
            player.GetComponent<PlayerWeapon>().enabled = false;
            this.enabled = false;
        }
    }
}
