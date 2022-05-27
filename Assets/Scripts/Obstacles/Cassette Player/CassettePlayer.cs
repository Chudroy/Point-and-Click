using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public enum CassetteType { Blue, Red, Gray, Orange, Green, Black }

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] InteractableButton button;
    CassetteSlot[] casseteSlots;
    CassetteType[] casseteSolutionArray;
    PlaceableItem[] cassetteItems;
    public static Action<string> LogCassettePlayerMessage;
    AudioSource cassetteAudioPlayer;
    Coroutine trySolveRoutine;


    private void Awake()
    {
        cassetteItems = new PlaceableItem[6];
        casseteSlots = GetComponentsInChildren<CassetteSlot>();
        cassetteAudioPlayer = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        button.PressButton += TrySolve;
    }

    private void OnDisable()
    {
        button.PressButton -= TrySolve;
    }

    void Start()
    {
        casseteSolutionArray = (CassetteType[])Enum.GetValues(typeof(CassetteType));
    }

    void TrySolve()
    {
        if (trySolveRoutine != null) return;
        trySolveRoutine = StartCoroutine(SolveRoutine());
    }

    IEnumerator SolveRoutine()
    {
        foreach (CassetteSlot slot in casseteSlots)
        {
            if (slot._currentCassetteModel == null)
            {
                LogCassettePlayerMessage?.Invoke("Won't play without a cassette in each slot");
                trySolveRoutine = null;
                yield break;
            }
        }

        GetCassetteItems();
        ToggleCollectorScripts(false);

        yield return StartCoroutine(PlayTapes());

        if (!CheckSolution())
        {
            FinishRoutine();
            yield break;
        }

        LogCassettePlayerMessage?.Invoke("Solved!");
        FinishRoutine();
    }

    void ToggleCollectorScripts(bool t)
    {
        foreach (CassetteSlot slot in casseteSlots)
        {
            Collector collector = slot._currentCassetteModel.GetComponent<Collector>();
            Debug.Log("Collector: " + collector);
            collector._canPickUp = t;
        }
    }

    void GetCassetteItems()
    {
        for (int i = 0; i < casseteSlots.Length; i++)
        {
            GameObject currentModel = casseteSlots[i]._currentCassetteModel;
            Pickup pickup = currentModel.GetComponent<Pickup>();
            PlaceableItem cassetteItem = pickup._item as PlaceableItem;

            cassetteItems[i] = cassetteItem;
        }
    }

    private IEnumerator PlayTapes()
    {


        AudioClip[] tapeClips = new AudioClip[casseteSlots.Length];

        for (int i = 0; i < casseteSlots.Length; i++)
        {
            // CassetteItem cassetteItem = casseteSlots[i]._currentCassetteModel.GetComponent<Pickup>()._item as CassetteItem;
            tapeClips[i] = cassetteItems[i]._tapeClip;
        }

        foreach (AudioClip tape in tapeClips)
        {
            cassetteAudioPlayer.clip = tape;
            cassetteAudioPlayer.Play();
            yield return new WaitForSeconds(tape.length);
        }
    }

    bool CheckSolution()
    {

        int idx = 0;

        foreach (CassetteSlot slot in casseteSlots)
        {
            // CassetteItem cassetteItem = slot._currentCassetteModel.GetComponent<Pickup>()._item as CassetteItem;

            if (cassetteItems[idx]._cassetteType != casseteSolutionArray[idx])
            {
                LogCassettePlayerMessage?.Invoke("Incorrect Order");
                return false;
            }

            idx++;
        }
        return true;
    }

    private void FinishRoutine()
    {
        trySolveRoutine = null;
        ToggleCollectorScripts(true);
    }
}


