using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

using UnityEngine.UI;
public class NumberField : MonoBehaviour
{
   private Board board;
   //Coords
   private int x1, y1;
   private int value;

   private string identifier;

   public Text number;

   private void Awake()
   {
      number = GetComponentInChildren<Text>();
   }

   public void SetValues(int _x1, int _y1, int _value, string _identifier, Board _board)
   {
      x1 = _x1;
      y1 = _y1;
      value = _value;
      identifier = _identifier;
      board = _board;

      number.text = (value != 0) ? value.ToString() : "";

      if (value != 0)
      {
         GetComponentInParent<Button>().interactable = false;
      }
      else
      {
         number.color = Color.blue;
      }
   }
}
