using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficulityAdjustment : MonoBehaviour
{
    ExtraMainTowerAttributes extraMainTowerAttributes;

    private void Awake()
    {
        extraMainTowerAttributes = FindObjectOfType<ExtraMainTowerAttributes>();
    }

    public float CalculateTimePoints(float time)
    {
        if (time > 300) return 0.1f;
        else if (time > 240 && time < 300) return 0.5f;
        else if (time > 120 && time < 240) return 0.8f;
        else if (time > 60 && time < 120) return 0.9f;
        else return 1f;
    }

    public float CalculateHpPoints(float HP)
    {
        return HP / extraMainTowerAttributes.startingHP;
    }

    public int GetIncrementSpawnRate(float time, float HP)
    {
        float timePoints = CalculateTimePoints(time);
        float hpPoints = CalculateHpPoints(HP);

        float tiers = (hpPoints * timePoints) / 1;

        if (tiers > 0.8f && tiers <= 1f) return 5;
        else if (tiers > 0.5f && tiers < 0.8f) return 3;
        else if (tiers > 0.3f && tiers < 0.5f) return 2;
        else return 1;
    }


}
