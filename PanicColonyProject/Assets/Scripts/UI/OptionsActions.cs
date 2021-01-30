using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsActions : MonoBehaviour
{
    public TMP_Dropdown resolutionDD;
    public TMP_Dropdown qualitiesDD;
    public TMP_Dropdown textureQualitiesDD;
    public TMP_Dropdown antiAliasingDD;
    public Toggle fullscreenTog;
    public Slider mastVolSlider;
    string mainVolumeName = "MainVolume";
    public AudioMixer mainMixer;
    public MenuManager manager;
    public AudioSource sound;
    public AudioClip backClip;
    public AudioClip saveClip;
    bool isUpdating = false;

    string[] qualityLevel = new string[4]
    {
        "Low",
        "Medium",
        "High",
        "Custom"
    };

    string[] textureLevel = new string[4]
    {
        "Full Res",
        "Half Res",
        "Quarter Res",
        "Eighth Res",
    };

    string[] antiAlisingLevel = new string[3]
    {
        "Disabled",
        "2x",
        "4x",
    };

    void Awake()
    {
        int selectedResolution = SetResolutionSetting();
        SetQualitySetting();
        SetTextureQualitySetting();
        SetAntiAliasingSetting();
        LoadValues(selectedResolution);
    }



    void LoadValues(int selectedResolution)
	{
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDD.value = PlayerPrefs.GetInt("Resolution");
            if (PlayerPrefs.HasKey("Fullsceen"))
            {
                fullscreenTog.isOn = (1 == PlayerPrefs.GetInt("Fullsceen"));
            }
            SetResolution(resolutionDD.value);
        }
        else
        {
            resolutionDD.value = selectedResolution;
            
            fullscreenTog.isOn = Screen.fullScreen;
        }

        if (PlayerPrefs.HasKey("Quality"))
        {
            qualitiesDD.value = PlayerPrefs.GetInt("Quality");
        }
        else
        {
            qualitiesDD.value = QualitySettings.GetQualityLevel();
        }

        if (PlayerPrefs.HasKey("Texture Quality"))
        {
            textureQualitiesDD.value = PlayerPrefs.GetInt("Texture Quality");
        }
        else
        {
            textureQualitiesDD.value = QualitySettings.masterTextureLimit;
        }

        if (PlayerPrefs.HasKey("Anti-Aliasing"))
        {
            antiAliasingDD.value = PlayerPrefs.GetInt("Anti-Aliasing");
        }
        else
        {
            textureQualitiesDD.value = QualitySettings.antiAliasing;
        }

        if (PlayerPrefs.HasKey("Main Volume"))
        {
            mastVolSlider.value = PlayerPrefs.GetFloat("Main Volume");
        }
        else
        {
            float volume = 0;
            mainMixer.GetFloat(mainVolumeName, out volume);
            mastVolSlider.value = volume;
        }

    }

    int SetResolutionSetting()
    {
        resolutionDD.ClearOptions();
        List<TMP_Dropdown.OptionData> tmpResolutions = new List<TMP_Dropdown.OptionData>();
        int selectedResolution = 0;
        foreach (Resolution res in Screen.resolutions)
        {
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                selectedResolution = tmpResolutions.Count;
            }
            tmpResolutions.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
        }

        resolutionDD.options = tmpResolutions;
        return selectedResolution;
    }
    public void SetResolution(int index)
	{
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }

    void SetQualitySetting()
    {
        qualitiesDD.ClearOptions();
        List<TMP_Dropdown.OptionData> tmpQualities = new List<TMP_Dropdown.OptionData>();
        foreach (string quality in qualityLevel)
        {
            tmpQualities.Add(new TMP_Dropdown.OptionData(quality));
        }
        qualitiesDD.options = tmpQualities;
    }

    public void SetQuality(int index)
    {
        if (index == 3)
            return;
        QualitySettings.SetQualityLevel(index);
        isUpdating = true;
        textureQualitiesDD.value = QualitySettings.masterTextureLimit;
        antiAliasingDD.value = QualitySettings.antiAliasing;
    }

    public void SetFullscreen(bool value)
	{
        Screen.fullScreen = value;
	}

    void SetTextureQualitySetting()
	{
        textureQualitiesDD.ClearOptions();
        List<TMP_Dropdown.OptionData> tmpTexQualities = new List<TMP_Dropdown.OptionData>();
        foreach (string quality in textureLevel)
        {
            tmpTexQualities.Add(new TMP_Dropdown.OptionData(quality));
        }
        textureQualitiesDD.options = tmpTexQualities;

    }

    public void SetTextureQuality(int index)
	{
        QualitySettings.masterTextureLimit = index;
        if (!isUpdating)
            qualitiesDD.value = 3;
    }

    void SetAntiAliasingSetting()
    {
        antiAliasingDD.ClearOptions();
        List<TMP_Dropdown.OptionData> tmpAntiAlias = new List<TMP_Dropdown.OptionData>();
        foreach (string antiAlisLevel in antiAlisingLevel)
        {
            tmpAntiAlias.Add(new TMP_Dropdown.OptionData(antiAlisLevel));
        }
        antiAliasingDD.options = tmpAntiAlias;

    }

    public void SetAntiAliasing(int index)
    {
        QualitySettings.antiAliasing = index * 2;
        if (!isUpdating)
            qualitiesDD.value = 3;
        isUpdating = false;
    }

    public void SetVolume(float value)
	{
        mainMixer.SetFloat(mainVolumeName, value);
	}

    public void OnSaveSettings()
	{
        PlayerPrefs.SetInt("Resolution", resolutionDD.value);
        PlayerPrefs.SetInt("Quality", qualitiesDD.value);
        PlayerPrefs.SetInt("Texture Quality", textureQualitiesDD.value);
        PlayerPrefs.SetInt("Fullsceen", fullscreenTog.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Anti-Aliasing", antiAliasingDD.value);
        PlayerPrefs.SetFloat("Main Volume", mastVolSlider.value);
        sound.PlayOneShot(saveClip);
    }

    public void OnBack()
	{
        manager.SetGUIState(MenuManager.GUIState.MENU);
        sound.PlayOneShot(backClip);
	}

    public void SetCustom()
	{
        qualitiesDD.value = 3;
	}
}
