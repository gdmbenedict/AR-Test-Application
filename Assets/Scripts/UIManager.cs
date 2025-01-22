using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject regularUI;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private GameObject rendersManagerObject; //object used to enable or disable the renders manager and attached camera

    [Header("Transition Variables")]
    [SerializeField] private float backgroundTransitionTime;
    [SerializeField] private float fieldPopupTime;

    [Header("Info Screen Elements")]
    [SerializeField] private CanvasGroup infoBackgroundGroup;
    [SerializeField] private Image infoBackgroundTile;
    [SerializeField] private GameObject exitMenuButton;

    [Header("Info Fields")]
    [SerializeField] private GameObject frontView;
    [SerializeField] private GameObject sideView;
    [SerializeField] private GameObject[] infoFields = new GameObject[6];
    [SerializeField] private TextMeshProUGUI[] infoFieldsText = new TextMeshProUGUI[6];

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    [SerializeField] private float pitchShift;
    [SerializeField] private AudioClip menuOn;
    [SerializeField] private AudioClip menuOff;
    [SerializeField] private AudioClip elementOn;

    private bool inInfoScreen = false;

    //Method that toggles the UI between the "gameplay" state and info state
    public void SwitchUI()
    {
        //shut down any currently operating coroutines
        StopAllCoroutines();

        //toggle state
        inInfoScreen = !inInfoScreen;
        //Debug.Log(inInfoScreen);

        if (inInfoScreen)
        {
            //variable for storing the current step the UI is on
            IEnumerator currentStep;

            infoUI.SetActive(true); //activating info UI

            PlaySound(menuOn); //playing info screen on sound
            currentStep = TransitionBackground(); //store coroutine to check if running
            StartCoroutine(TransitionBackground()); //transition background

            //wait until coroutine is done
            while (currentStep.MoveNext()) Debug.Log("waiting");

            //add exit button
            exitMenuButton.SetActive(true);

            //activate the individual fields
            StartCoroutine(ActivateFields());

            regularUI.SetActive(false); //de-activating regular UI
        }
        else
        {
            regularUI.SetActive(true); //activating regular UI

            PlaySound(menuOff); //playing info screen off sound

            //turning off render textures
            frontView.SetActive(false);
            sideView.SetActive(false);

            //turning off info Fields
            foreach (GameObject infoField in infoFields)
            {
                infoField.SetActive(false);
            }

            //remove exit button
            exitMenuButton.SetActive(false);

            //transition UI backgrounds
            StartCoroutine(TransitionBackground());

            infoUI.SetActive(false); //de-activating info UI
            rendersManagerObject.SetActive(true); //turning on renders and render textures
        }
    }

    //Method that updates all of the UI infomation fields
    public void UpdateInfo(string[] infoText)
    {
        //loop through array updating information
        for (int i =0; i<infoFieldsText.Length; i++)
        {
            infoFieldsText[i].text = infoText[i].Replace("\\n", "\n");
        }
    }

    //Method that transitions background to the correct state
    private IEnumerator TransitionBackground()
    {
        //declaring temp values
        float startValue;
        float endValue;
        float timer = 0;
        float currentValue;

        //setting initial values
        if (inInfoScreen)
        {
            startValue = 0;
            endValue = 1;
        }
        else
        {
            startValue = 0;
            endValue = 1;
        }

        //modify background alpha over a set time
        while (timer < backgroundTransitionTime)
        {
            //calc current value
            currentValue = Mathf.Lerp(startValue, endValue, timer/ backgroundTransitionTime);

            //apply to UI elements
            infoBackgroundGroup.alpha = currentValue;
            infoBackgroundTile.transform.localScale = new Vector3(1, currentValue, 1);

            //incrtement timer
            timer += Time.deltaTime;
            yield return null;
        }

        //snap to final value
        infoBackgroundGroup.alpha = endValue;
        infoBackgroundTile.transform.localScale = new Vector3(1, endValue, 1);
    }
    
    //Method that activates fields one by one
    private IEnumerator ActivateFields()
    {
        //activate the mech views and play sound
        PlaySound(elementOn);
        frontView.SetActive(true);
        sideView.SetActive(true);

        foreach (GameObject infoField in infoFields)
        {
            //wait for set time then activate field and play sound
            yield return new WaitForSeconds(fieldPopupTime);
            PlaySound(elementOn);
            infoField.SetActive(true);
        }
    }

    //function to play sound
    private void PlaySound(AudioClip audioClip)
    {
        source.pitch = 1 + Random.Range(-pitchShift, pitchShift);
        source.PlayOneShot(audioClip);
    }

    public bool getInfoScreenOn()
    {
        return inInfoScreen;
    }
}
