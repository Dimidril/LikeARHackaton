using System;
using API.Helpers;
using Plugins.ARFoundationPlaceModelOnGround.Scripts.AR;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestAddingModel : MonoBehaviour
    {
        [SerializeField] private ArTapToPlaceObject arTapToPlaceObject;
        [SerializeField] private string modelLink;

        private void Start()
        {
            ModelLoader.DownloadModel(modelLink, () =>
            {
                arTapToPlaceObject.Init(ModelLoader.GetModel(modelLink));
            });
        }
    }
}