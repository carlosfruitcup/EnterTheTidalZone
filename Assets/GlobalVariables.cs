using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Kino;
using UnityEngine.Rendering.PostProcessing;

public class GlobalVariables : MonoBehaviour
{
    public bool busy = false;
    public bool isIntro = false;
    public bool rewindTime = false;
    public static GlobalVariables global;
    public ScreenFade fadeHandler;
    public MapHandler mapHandler;
    public GameObject pauseHandler;
    public SettingsHandler settingsHandler;
    public TextMeshProUGUI copyright;
    public DigitalGlitch digitalGlitch;
    public AnalogGlitch analogGlitch;
    public PostProcessVolume postProcessing;
    public PostProcessLayer postProcessLayer;
    public LeftJoystick joystick;
    public BgmHandler bgmHandler;
    void Update()
    {
        if(!busy)
        {
            if(!isIntro)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseHandler.SetActive(true);
                    busy = true;
                    PauseTime(true);
                    bgmHandler.FadePauseDisablers(true);
                }
                /*else if(Input.GetKeyDown(KeyCode.Return))
                {
                    mapHandler.gameObject.SetActive(true);
                    mapHandler.ShowMap();
                    busy = true;
                }*/
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseHandler.activeSelf == true)
        {
            PauseTime(false);
            pauseHandler.SetActive(false);
            busy = false;
            bgmHandler.FadePauseDisablers(false);
        }
    }
    void Awake()
    {
        if (global == null)
        {
            global = this;
            fadeHandler = transform.Find("Fade").GetComponent<ScreenFade>();
            mapHandler = transform.Find("Map").GetComponent<MapHandler>();
            pauseHandler = transform.Find("Pause").gameObject;
            settingsHandler = transform.Find("Settings").GetComponent<SettingsHandler>();
            copyright = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            joystick = transform.Find("GameButtons").Find("Left Joystick").GetComponent<LeftJoystick>();
            bgmHandler = GetComponent<BgmHandler>();
            #if UNITY_ANDROID
            Debug.Log("Android");
            #else
            joystick.gameObject.SetActive(false);
            transform.Find("GameButtons").Find("Jump").gameObject.SetActive(false);
            #endif
            if(!isIntro) {
                digitalGlitch = GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>();
                analogGlitch = GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>();
                postProcessing = GameObject.FindWithTag("Post Processing").GetComponent<PostProcessVolume>();
                postProcessLayer = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessLayer>();
            }
            if(SceneManager.GetActiveScene().name != "title")
            {
                StartCoroutine(fadeHandler.Fade(true,0.5f));
                if(busy) StartCoroutine(FadeAndUnbusy());
            }
        }
    }
    void Start () {
        int width = 1024; // or something else
        int height= 576; // or something else
        bool isFullScreen = false; // should be windowed to run in arbitrary resolution
        int desiredFPS = 60; // or something else
        Screen.SetResolution (width , height, isFullScreen, desiredFPS );
        Settings.Initialize();
    }
    public void Fade(string scene)
    {
        StartCoroutine(fadeHandler.Fade(false,0.5f));
        StartCoroutine(FadeAndLoad(scene));
    }
    public IEnumerator FadeAndUnbusy()
    {
        yield return new WaitForSeconds(0.5f);
        busy = false;
    }
    public IEnumerator FadeAndLoad(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        global = null;
        SceneManager.LoadScene(scene);
    }
    public void Busy(bool b)
    {
        busy = b;
    }
    public void MusicMute(bool mute)
    {
        if(mute) bgmHandler.Pause();
        else bgmHandler.UnPause();
    }
    public void PauseTime(bool toggle)
    {
        if(toggle) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void ToggleParticles(bool toggle)
    {
        foreach(ParticleSystem particles in Resources.FindObjectsOfTypeAll<ParticleSystem>())
        {
            /*
            if(toggle) particles.Play();
            else particles.Stop();
            */
            particles.gameObject.SetActive(toggle);
        }
    }
}
