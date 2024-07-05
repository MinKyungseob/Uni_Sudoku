using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    int[,] solveGrid = new int [9,9];
    string s;
    int[,] riddleGrid = new int [9,9];
    public int piecesToErase = 35;
    public Transform[] parts;

    public Transform Part11, Part12, Part13, Part21, Part22, Part23, Part31, Part32, Part33;
    public GameObject buttonPrefab;

    private List<NumberField> fieldList = new List<NumberField>();
    
    //Difficulty
    public enum Difficulties
    {
        DEBUG,
        EASY,
        MEDIUM,
        HARD,
        INSANE
    }

    public Difficulties difficulty;
    private int maxHints = 5;
    
    private void Awake()
    {
        parts = GetComponentsInChildren<Transform>();
    }
    
    void Start()
    {
        InitGrid(ref solveGrid);
        //DebugGrid(ref solveGrid);

        ShuffleGrid(ref solveGrid, 5);
        CreateRiddleGrid();
        CreateButtons();
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
        int sep = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s += grid[i, j].ToString();

                sep = j % 3;
                if (sep == 2)
                {
                    s += "|";
                }

                if (i % 3 == 2 && j%9 == 8)
                {
                    s +="\n";
                }
            }
            s += "\n";
        }
        print(s);
    }

    void ShuffleGrid(ref int[,] grid, int shuffleAmount)
    {
        for (int i = 0; i < shuffleAmount; i++)
        {
            int value1 = Random.Range(1, 10);
            int value2 = Random.Range(1, 10);
            //mix 2 cells
            MixTwoGridCells(ref grid,value1, value2);
        }
        DebugGrid(ref grid);
    }

    void MixTwoGridCells(ref int[,] grid, int value1, int value2)
    {
        int x1 = 0;
        int x2 = 0;
        int y1 = 0;
        int y2 = 0;

        for (int i = 0; i < 9; i+=3)
        {
            for (int k = 0; k < 9 ;k+=3)
            {
                for (int j = 0; j <3 ; j++)
                {
                    for (int l = 0; l <3; l++)
                    {
                        if (grid[i + j, k + l] == value1)
                        {
                            x1 = i + j;
                            y1 = k + l;
                        }
                        if (grid[i + j, k + l] == value2)
                        {
                            x2 = i + j;
                            y2 = k + l;
                        }
                    }
                }
                grid[x1, y1] = value2;
                grid[x2, y2] = value1;
            }   
        }
    }

    void CreateRiddleGrid()
    {
        //Copy the SolveGrid
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                riddleGrid[i, j] = solveGrid[i, j];
            }
        }
        //Set Difficulty
        SetDifficulty();
        
        //Erase From riddleGrid
        for (int i = 0; i < piecesToErase; i++)
        {
            int x1 = Random.Range(0, 9);
            int y1 = Random.Range(0, 9);
            //Reroll Until We find one without A 0
            while (riddleGrid[x1, y1] == 0)
            {
                x1 = Random.Range(0, 9);
                y1 = Random.Range(0, 9);
            }
            //Once We Found One with No 0
            riddleGrid[x1, y1] = 0;
        }
        DebugGrid(ref riddleGrid);
    }

    void CreateButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j <9; j++)
            {
                GameObject newButton = Instantiate(buttonPrefab);
                
                //Set all Values of each grid
                NumberField numberField = newButton.GetComponent<NumberField>();
                numberField.SetValues(i, j, riddleGrid[i, j], i + "," + j, this);
                newButton.name = i + "," + j;

                if (riddleGrid[i, j] == 0)
                {
                    fieldList.Add(numberField);
                }
                
                //parent of the button
                if (i < 3)
                {
                    if (j < 3)
                    {
                        newButton.transform.SetParent(Part11, false);
                    }

                    if (j > 2 && j < 6)
                    {
                        newButton.transform.SetParent(Part12, false);
                    }

                    if (j > 5)
                    {
                        newButton.transform.SetParent(Part13, false);
                    }
                }

                if (i > 2 && i < 6)
                {
                    if (j < 3)
                    {
                        newButton.transform.SetParent(Part21, false);
                    }

                    if (j > 2 && j < 6)
                    {
                        newButton.transform.SetParent(Part22, false);
                    }

                    if (j > 5)
                    {
                        newButton.transform.SetParent(Part23, false);
                    }
                }

                if (i > 5)
                {
                    if (j < 3)
                    {
                        newButton.transform.SetParent(Part31, false);
                    }

                    if (j > 2 && j < 6)
                    {
                        newButton.transform.SetParent(Part32, false);
                    }

                    if (j > 5)
                    {
                        newButton.transform.SetParent(Part33, false);
                    }
                }
            }
        }
    }

    public void SetInputInRiddle(int x, int y, int value)
    {
        riddleGrid[x, y] = value;
    }
    
    void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulties.DEBUG:
                piecesToErase = 5;
                maxHints = 2;
                break;
            case Difficulties.EASY:
                piecesToErase = 35;
                maxHints = 4;
                break;
            case Difficulties.MEDIUM :
                piecesToErase = 40;
                maxHints = 6;
                break;
            case Difficulties.HARD:
                piecesToErase = 45;
                maxHints = 8;
                break;
            case Difficulties.INSANE:
                piecesToErase = 55;
                maxHints = 10;
                break;
        }
    }

    public void CheckComplete()
    {
        if (CheckIfWon())
        {
            print("You Won!");
        }
        else
        {
            print("Try Again ToT");
        }
    }

    bool CheckIfWon()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (riddleGrid[i, j] != solveGrid[i, j])
                    return false;
            }
            
        }
        return true;
    }

    public void ShowHint()
    {
        if (fieldList.Count > 0 && maxHints > 0)
        {
            int randIndex = Random.Range(0, fieldList.Count);

            maxHints--;
            riddleGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()] =
                solveGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()];
            
            fieldList[randIndex].SetHint( riddleGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()]);
            
            fieldList.RemoveAt(randIndex);
        }
        else
        {
            print("No Hints Left");
        }
    }
}
