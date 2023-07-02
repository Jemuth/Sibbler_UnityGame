using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillCooldownP1 : MonoBehaviour
{
    [SerializeField] public Image abilityImage1;
    public float cooldownTime = 8f;
    public float cooldownTimer = 0f;
    public bool skill1Used;
    private bool skillOnCooldown;
    void Start()
    {
        abilityImage1.fillAmount = 0;
        skill1Used = false;
        skillOnCooldown = false;
    }
    public void UISkillChecker(bool skillChecker)
    {
        skill1Used = skillChecker;
    }
    private void UICooldown()
    {
        if (skill1Used == true && !skillOnCooldown)
        {
            skillOnCooldown = true;
            cooldownTimer = 0f;
        }

        if (skillOnCooldown)
        {
            if (cooldownTimer <= cooldownTime)
            {
                cooldownTimer += Time.deltaTime;
                abilityImage1.fillAmount = 1 - (cooldownTimer / cooldownTime);
            }
            else
            {
                abilityImage1.fillAmount = 0;
                skillOnCooldown = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UICooldown();
        UISkillChecker(skill1Used);
    }
}
