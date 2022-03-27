using System;
using System.Collections;
using System.Collections.Generic;
using API;
using API.Helpers;
using Plugins.ARFoundationPlaceModelOnGround.Scripts.AR;
using UnityEngine;
using Wrappers = API.Wrappers;

public class ModelsCardsConvas : MonoBehaviour
{
    [SerializeField] private ArTapToPlaceObject _tapToPlaceObject;

    public void ChangeActiveModel(GameObject modelInfo)
    {
        _tapToPlaceObject.Init(modelInfo);
    }
}
