using UnityEngine;
/// <summary>Loads settings from PlayerPrefs.
/// <para>This is a static class. </para>
/// <seealso cref="SettingsHandler"/>
/// </summary>
public static class Settings
{
    public static bool GetSetting(int option)
    {
        return Common.IntToBool(PlayerPrefs.GetInt("option"+option));
    }
    public static void Initialize()
    {
        if(!Common.IntToBool(PlayerPrefs.GetInt("firstlaunch")))
        {
            Debug.Log("first time launch");
            PlayerPrefs.SetInt("firstlaunch",1);
            PlayerPrefs.SetInt("option0",1);
            PlayerPrefs.SetInt("option1",1);
            PlayerPrefs.SetInt("option2",1);
            PlayerPrefs.SetInt("option3",1);
            PlayerPrefs.SetInt("option4",1);
            //PlayerPrefs.SetInt("option5",1);
        }
        GlobalVariables.global.postProcessing.enabled = GetSetting(0);
        GlobalVariables.global.postProcessLayer.enabled = GetSetting(0);
        GlobalVariables.global.analogGlitch.enabled = GetSetting(1);
        GlobalVariables.global.digitalGlitch.enabled = GetSetting(1);
        GlobalVariables.global.copyright.gameObject.SetActive(GetSetting(2));
        #if UNITY_ANDROID
        GlobalVariables.global.joystick.gameObject.SetActive(GetSetting(3));
        #endif
        GlobalVariables.global.ToggleParticles(GetSetting(4));
        //continue after more settings are added
    }
}
