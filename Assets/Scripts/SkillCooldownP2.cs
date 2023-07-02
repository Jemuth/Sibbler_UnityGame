using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillCooldownP2 : MonoBehaviour
{
    [SerializeField] public Image abilityImage1;
    public float cooldownTime = 15f;
    public float cooldownTimer = 0f;
    public bool skill2Used;
    private bool skillOnCooldown;
    void Start()
    {
        abilityImage1.fillAmount = 0;
        skill2Used = false;
        skillOnCooldown = false;
    }
    public void UISkillChecker2(bool skillChecker)
    {
        skill2Used = skillChecker;
    }
    private void UICooldown2()
    {
        if (skill2Used == true && !skillOnCooldown)
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
        UICooldown2();
        UISkillChecker2(skill2Used);
    }
}
