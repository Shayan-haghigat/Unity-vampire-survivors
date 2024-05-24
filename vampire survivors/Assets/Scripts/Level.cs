using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    int level = 1;
    [SerializeField] ExperienceBar experienceBar;
    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }
    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience,TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
    }
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUP();
        experienceBar.UpdateExperienceSlider(experience,TO_LEVEL_UP);
    }
    public void CheckLevelUP()
    {
        if (experience >=TO_LEVEL_UP) { 
            experience -= TO_LEVEL_UP;
            level += 1;
            experienceBar.SetLevelText(level);
        }
    }
}