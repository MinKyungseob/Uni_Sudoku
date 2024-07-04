using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour
{
    public static InputField instance;

    private NumberField lastField;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateInputField(NumberField field)
    {
        this.gameObject.SetActive(true);
        lastField = field;
    }

    public void ClickedInput(int number)
    {
        lastField.ReceiveInput(number);
        //Deactivate Panel
        this.gameObject.SetActive(false);
    }
}
