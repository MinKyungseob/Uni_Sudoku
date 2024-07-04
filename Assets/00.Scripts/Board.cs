using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Board : MonoBehaviour
{
    int[,] solveGrid = new int [9,9];
    string s;
    // Start is called before the first frame update
    void Start()
    {
        InitGrid(ref solveGrid);
        DebugGrid(ref solveGrid);
    }

    void InitGrid(ref int[,] grid)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1; //grid (3,3) 짜리로 체크 하기 위한 방법. 수도쿠는 각 줄마다1 ~9 인것 뿐만 아니라 (3,3)사이즈를 기준으로도 1~9를 체크
            }
        }

        int n1 = 8 * 3;     //24
        int n2 = 8 / 3;     //2
        int n = (n1 + n2 + 0) % 9 + 1;
        print(n1 + " + " + n2 + "+" + 0);
        print(n);
    }

    void DebugGrid(ref int[,] grid)
    {
        s = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s += grid[i, j].ToString();
            }

            s += "\n";
        }
        print(s);
    }
}
