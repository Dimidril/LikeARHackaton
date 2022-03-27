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

    public Wrappers.ARObject ARObject { get; private set; }
    public ModelsCardsConvas Parent { get; private set; }

    private void Awake()
    {
        mainButton.onClick.AddListener(() => Parent.ChangeActiveModel(ARObject));
    }

    public void Init(ModelsCardsConvas parent, Wrappers.ARObject model)
    {
        Parent = parent;
    }
}
