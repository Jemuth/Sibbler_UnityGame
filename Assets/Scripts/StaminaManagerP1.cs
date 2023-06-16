using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManagerP1 : MonoBehaviour
{
    public Slider staminaBar;

    private float maxStamina;
    public float currentStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    [SerializeField] private PlayableCharacterData m_data;
    private Coroutine regen;
    public static StaminaManagerP1 instance;

    private void Awake()
    {
        instance = this;
        maxStamina = m_data.maxStamina;
    }
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not enough stamina");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(5);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;

        }
        regen = null;
    }
}
