using UnityEngine;

public static class CalculateValueForModificator
{
    public static float Calculate(float modificatorValue) =>
       Mathf.Max(1 - modificatorValue, 0);
}