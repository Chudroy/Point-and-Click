using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Computer : Interactable
{
    Keyboard keyboard;
    Display display;
    Text text;
    public static bool active = false;
    public static Action<bool> SetPlayKeyboardInput;

    private void Awake()
    {
        keyboard = GetComponentInChildren<Keyboard>();
        display = GetComponentInChildren<Display>();
        text = GetComponentInChildren<Text>();
    }
    public override void Start()
    {
        base.Start();
        Terminal.ClearScreen();
        EnableComputer(false);
    }
    public override async void OnPointerClick(PointerEventData eventData)
    {
        if (active) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        EnableComputer(true);

        await Terminal.WriteLine("Cargando… ");
        await Task.Delay(2000);
        await Terminal.WriteLine("Computadora de ultraprocesamiento cuántico MARK-31. ");
        await Terminal.WriteLine("Procesamientos en segundo plano: Compilación de datos historicos y sintetización del saber.");
        await Task.Delay(500);
        await Terminal.WriteLine("/ C:/");
    }

    public override void LeaveInteractable()
    {
        Terminal.ClearScreen();
        EnableComputer(false);
    }

    void EnableComputer(bool t)
    {
        keyboard.enabled = t;
        active = t;
        DisplayBuffer.active = t;
        SetPlayKeyboardInput?.Invoke(!t);
    }
}
