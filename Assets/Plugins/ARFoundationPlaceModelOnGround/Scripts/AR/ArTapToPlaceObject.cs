using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Plugins.ARFoundationPlaceModelOnGround.Scripts.AR
{
    public class ArTapToPlaceObject : MonoBehaviour
    {
        readonly Vector2 SCREEN_CENTER_VECTOR = Vector2.one * 0.5f;

        [CanBeNull] public static event System.Action OnObjectPlaced;
        [CanBeNull] public static event System.Action OnPlaneFinded;
        [CanBeNull] public static event System.Action OnPlaneLosed;

        [SerializeField] ARRaycastManager arRaycastManager;
        [SerializeField] GameObject placementIndicator;
        GameObject objectToPlace;
        [SerializeField] private Transform parentForArObject;
        [SerializeField] Camera arCamera;
        [SerializeField] ARPlaneManager arPlaneManager;
        [SerializeField] Camera virtualCamera;

        Pose placementPose = new Pose(Vector3.zero, Quaternion.identity);
        bool placementPoseIsValid = false;

        void Start()
        {
            PlaneLosed();
        }

        static void PlaneLosed()
        {
            OnPlaneLosed?.Invoke();
        }

        void Update()
        {
#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
                //gameObject.SetActive(false);
                virtualCamera.gameObject.SetActive(true);
                arCamera.gameObject.SetActive(false);
            }

            return;
#endif

            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (placementPoseIsValid)
            {
                OnPlaneFinded?.Invoke();

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    PlaceObject();
                    placementIndicator.gameObject.SetActive(false);
                    arPlaneManager.enabled = false;

                    foreach (var groundPoint in FindObjectsOfType<ARFeatheredPlaneMeshVisualizer>())
                        Destroy(groundPoint.gameObject);

                    //gameObject.SetActive(false);
                }
            }
            else
            {
                PlaneLosed();
            }
        }

        public void Init(GameObject model)
        {
            objectToPlace = model;
            objectToPlace.transform.SetParent(parentForArObject);
            objectToPlace.SetActive(false);
        }

        public void Reset()
        {
            PlaneLosed();

            gameObject.SetActive(true);
            arPlaneManager.enabled = true;
            placementPoseIsValid = false;
            //objectToPlace.SetActive(false);
            arCamera.gameObject.SetActive(true);
            virtualCamera.gameObject.SetActive(false);
            objectToPlace = null;

            //objectToPlace.transform.position = Vector3.one * 10000; //Перемещаем подальше от глаз, отсюда 10000
        }

        void PlaceObject()
        {
            OnObjectPlaced?.Invoke();
            objectToPlace.SetActive(true);
            objectToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            Reset();
        }

        void UpdatePlacementPose()
        {
            var screenCenter = arCamera.ViewportToScreenPoint(SCREEN_CENTER_VECTOR);
            var hits = new List<ARRaycastHit>();
            arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            placementPoseIsValid = hits.Count > 0;

            if (!placementPoseIsValid)
                return;

            placementPose = hits[0].pose;

            var cameraForward = arCamera.transform.forward;
            var cameraBearing = (Vector3.right * cameraForward.x + Vector3.forward * cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }

        void UpdatePlacementIndicator()
        {
            placementIndicator.SetActive(placementPoseIsValid);

            if (placementPoseIsValid)
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }

    public class Wrappers
    {
    }
}