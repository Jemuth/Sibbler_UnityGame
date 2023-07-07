using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionManager : MonoBehaviour
{
    public Slider detectionBar;
    private int maxDetection = 100;
    public int currentDetection;
    private WaitForSeconds regenTick = new WaitForSeconds(0.05f);
    private Coroutine regen;
    private bool detected;
    private bool enemyContact;
    private float perceptionThreshold = 1.5f;
    private float perceptionTime = 0f;


    void Start()
    {
        currentDetection = 0;
        detectionBar.maxValue = maxDetection;
        detectionBar.value = 0;
    }
    public void DetectionBarChecker(bool p_detected)
    {
        detected = p_detected;
    }
    public void ContactChecker(bool p_contact)
    {
        enemyContact = p_contact;
    }
    public void EnemyContact()
    {
        if (enemyContact)
        {
            if(perceptionTime < perceptionThreshold)
            {
                perceptionTime += Time.deltaTime;
            }
            if (perceptionTime >= perceptionThreshold)
            {
                currentDetection += 1;
                detectionBar.value = currentDetection;

                if (regen != null)
                    StopCoroutine(regen);

                regen = StartCoroutine(RegenDetection());
            }
        }
        if (!enemyContact) 
        { 
            perceptionTime -= Time.deltaTime;
            if (perceptionTime <= 0)
            {
                perceptionTime = 0;
            }
        } 
    }
    public void Detection()
    {
        if (detected)
        {
            currentDetection += 2;
            detectionBar.value = currentDetection;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenDetection());
        }
    }
    
    public void Detected()
    {
        if(detectionBar.value >= 100)
        {
            GameManager.instance.CheckDetected(true);
        }
    }
    private IEnumerator RegenDetection()
    {
        yield return new WaitForSeconds(1);

        while (currentDetection != 0)
        {
            currentDetection -= maxDetection / 100;
            detectionBar.value = currentDetection;
            yield return regenTick;

        }
        regen = null;
    }
    private void Update()
    {
        Detection();
        Detected();
        EnemyContact();
    }
}
