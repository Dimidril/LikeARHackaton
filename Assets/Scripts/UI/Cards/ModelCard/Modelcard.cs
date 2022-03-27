using System;
using System.Collections;
using System.Collections.Generic;
using API;
using API.Helpers;
using UnityEngine;
using UnityEngine.UI;

public class Modelcard : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private GameObject Model;
    
    
    
    public Wrappers.ARObject ARObject { get; private set; }
    [SerializeField] public ModelsCardsConvas Parent;

    private void Awake()
    {
        mainButton.onClick.AddListener(() => Parent.ChangeActiveModel(Model));
    }

    public void Init(ModelsCardsConvas parent, Wrappers.ARObject model)
    {
        Parent = parent;
    }
}
