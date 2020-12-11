﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChordCreator : MonoBehaviour
{
    public GameObject chordPrefab;
    GameObject newChord;
    public GameObject chordsContainer;
    public bool anyChordsDragging;
    public Animator chordCreator;
    public Toggle chordCreatorToggle;
    public bool isOpen;
    Song song;

    void Start()
    {
        song = GameObject.FindGameObjectWithTag("Song").GetComponent<Song>();
    }

    void Update()
    {
        //Ver si hay algún chord dragging
        bool accum = false;
        foreach(Transform chord in chordsContainer.transform)
        {
            if(chord.gameObject.GetComponent<Chord>().dragging)
            {
                accum = true;
            }
        }
        if (accum) { anyChordsDragging = true; }
        else { anyChordsDragging = false; }

        //Si lo hay, modo asesino

        chordCreator.SetBool("Dragging", anyChordsDragging);
        

    }
    public void CreateChord(int nDegree)
    {
        foreach(Chord chord in song.chordsOnTheTable)
        {
            if (chord.degree == nDegree)
            {
                chord.gameObject.GetComponent<Animator>().SetTrigger("alreadyThere");
                return;
            }

        }
            Chord newChord = Instantiate(chordPrefab, chordsContainer.transform).GetComponent<Chord>();
            newChord.degree = nDegree;
            newChord.rectTransform.anchoredPosition += Vector2.right * (newChord.degree - 1) * (newChord.rectTransform.rect.width + 20) + Vector2.down * 100;
    }

    public void SetIsOpen(bool nIsOpen)
    {
        chordCreator.SetBool("ChordCreatorOpen", nIsOpen);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chord")
        {
            chordCreator.SetBool("Deleting", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chord")
        {
            chordCreator.SetBool("Deleting", false);
        }
    }
}
