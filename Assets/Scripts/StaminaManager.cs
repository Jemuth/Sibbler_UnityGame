using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Slider staminaBar;
    private int maxStamina;
    public float currentStamina;
    public bool canUseStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    [SerializeField] private PlayableCharacterData m_data;
    private Coroutine regen;

    private void Awake()
    {
        maxStamina = m_data.maxStamina;
    }
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }
    public void UseStamina()
    {
        if (currentStamina - 2 >= 0 && canUseStamina && Input.GetKey(KeyCode.LeftShift))
        {
            currentStamina -= 2;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            canUseStamina = false;
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
    private void Update()
    {
        UseStamina();
    }
}

