using UnityEngine;
using UnityEngine.UI;
/// <summary>Used for Global to apply settings within the pause menu.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/>
/// </summary>
public class SettingsHandler : MonoBehaviour
{
    public bool[] currentOptions = new bool[6];
    public bool[] defaultOptions = new bool[6];
    public Toggle[] checkmarks;
    public void Awake()
    {
        #if UNITY_ANDROID
        checkmarks[3].transform.parent.gameObject.SetActive(true);
        #else
        checkmarks[3].transform.parent.gameObject.SetActive(false);
        #endif
        bool[] optionPool = new bool[6];
        for(int i = 0; i < currentOptions.Length;i++)
        {
            optionPool[i] = IntToBool(PlayerPrefs.GetInt("option"+i,0));
        }
        if(optionPool != currentOptions) currentOptions = optionPool;
    }
    public void Start()
    {
        ResetCheckmarks();
        for(int i = 0; i < currentOptions.Length;i++)
        {
            defaultOptions[i] = currentOptions[i];
        }
        ApplySettings();
    }
    public void SetOption1(bool option)
    {
        currentOptions[0] = option;
    }
    public void SetOption2(bool option)
    {
        currentOptions[1] = option;
    }
    public void SetOption3(bool option)
    {
        currentOptions[2] = option;
    }
    public void SetOption4(bool option)
    {
        currentOptions[3] = option;
    }
    public void SetOption5(bool option)
    {
        currentOptions[4] = option;
    }
    public void SetOption6(bool option)
    {
        currentOptions[5] = option;
    }
    int BoolToInt(bool boolean)
    {
        if(boolean) return 1;
        else return 0;
    }
    bool IntToBool(int integer)
    {
        if(integer == 0) return false;
        else return true;
    }
    public void ApplySettings()
    {
        for(int i = 0; i < currentOptions.Length;i++)
        {
            PlayerPrefs.SetInt("option"+i,BoolToInt(currentOptions[i]));
            defaultOptions[i] = currentOptions[i];
        }
        PlayerPrefs.Save();
        Settings.Initialize();
    }
    public void ResetSettings()
    {
        for(int i = 0; i < defaultOptions.Length;i++)
        {
            currentOptions[i] = defaultOptions[i];
        }
        ResetCheckmarks();
    }
    void ResetCheckmarks()
    {
        for(int i = 0; i < currentOptions.Length;i++)
        {
            checkmarks[i].isOn = currentOptions[i];
        }
    }
}
