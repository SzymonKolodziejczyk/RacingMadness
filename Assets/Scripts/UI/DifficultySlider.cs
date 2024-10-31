using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    [SerializeField] private Slider skillLevelSlider;
    [SerializeField] public Text difficultyText;

    private void Start()
    {
        LoadSliderAIValue();
    }

    private void LoadSliderAIValue()
    {
        skillLevelSlider.value = PlayerPrefs.GetFloat("SkillLevel");
        SetSliderValue();
    }

    public void SetSliderValue()
    {
        float SkillLevelSliderValue = skillLevelSlider.value;

        if (SkillLevelSliderValue == 0){
            PlayerPrefs.SetFloat("SkillLevel", 0.8f);
            difficultyText.text = "Warming Up";
        }else if (SkillLevelSliderValue == 1){
            PlayerPrefs.SetFloat("SkillLevel", 1f);
            difficultyText.text = "Standard";
        }else if (SkillLevelSliderValue == 2){
            PlayerPrefs.SetFloat("SkillLevel", 1f);
            difficultyText.text = "Hard";
        }else if (SkillLevelSliderValue == 3){
            PlayerPrefs.SetFloat("SkillLevel", 1f);
            difficultyText.text = "Hot Wheels";
        }else if (SkillLevelSliderValue == 4){
            PlayerPrefs.SetFloat("SkillLevel", 1f);
            difficultyText.text = "Drift King";
        }
        //PlayerPrefs.Save();
    }

    /*private void Start()
    {
        skillLevelSlider.value = PlayerPrefs.GetFloat("difficultySlider");
        
        SetBackgroundVolume();
    }
    
    public void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("SkillLevel", value);
        PlayerPrefs.Save();
    }*/
}
