using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculateMovePosition
{
    private static Vector3[,] matrix;
    private static float step = 0.8f;

    public static Vector3[,] Calulate(int count, out int sqrt)
    {
        sqrt = (int)Mathf.Ceil(Mathf.Sqrt(count));
        matrix = new Vector3[sqrt, sqrt];
        var stepX = 0f;
        var stepZ = 0f;
        var offset = 0f;
        if (sqrt > 2) offset = (sqrt / 2) * step * -1f;
        stepX = offset;
        for (int i = 0; i < sqrt; i++)
        {
            for (int j = 0; j < sqrt; j++)
            {
                matrix[i, j] = new Vector3(stepX, 0.3f, stepZ);
                stepX += step;
            }
            stepX = offset;
            stepZ += step;
        }
        return matrix;
    }


}
