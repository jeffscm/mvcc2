﻿/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;
using System.Timers;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VuforiaSample
{
    public enum PLACEMENT { NOTRACKING, PLACING, NORMAL };

    public class GroundPlaneProxy : AppMonoController
    {

        public static Action<bool> OnStatusChange;
        public static Action OnDetectPositionOnGround;
        public static Action<bool> OnStartARGroundPlane;

        static TrackableBehaviour.Status StatusCached = TrackableBehaviour.Status.NO_POSE;
        static TrackableBehaviour.StatusInfo StatusInfoCached = TrackableBehaviour.StatusInfo.UNKNOWN;

        public static bool TrackingStatusIsTrackedAndNormal
        {
            get
            {
                return
                    (StatusCached == TrackableBehaviour.Status.TRACKED ||
                     StatusCached == TrackableBehaviour.Status.EXTENDED_TRACKED) &&
                    StatusInfoCached == TrackableBehaviour.StatusInfo.NORMAL;
            }
        }

        // Less Strict: Property returns true when Status is Tracked/Normal or Limited/Unknown.
        public static bool TrackingStatusIsTrackedOrLimited
        {
            get
            {
                return
                    ((StatusCached == TrackableBehaviour.Status.TRACKED ||
                     StatusCached == TrackableBehaviour.Status.EXTENDED_TRACKED) &&
                     StatusInfoCached == TrackableBehaviour.StatusInfo.NORMAL) ||
                    (StatusCached == TrackableBehaviour.Status.LIMITED &&
                     StatusInfoCached == TrackableBehaviour.StatusInfo.UNKNOWN);
            }
        }


        public PlaneFinderBehaviour planeFinder;
        public ContentPositioningBehaviour contentPositioningBehaviour;

        public GameObject baseGrounds;

        //public EventSystem eventSystem; // commented out as not using TAP to Place
        //public GraphicRaycaster graphicRayCaster;

        SmartTerrain smartTerrain;
        PositionalDeviceTracker positionalDeviceTracker;
        Timer timer;
        bool timerFinished;

        static bool GroundPlaneHitReceived { get; set; }
        int automaticHitTestFrameCount;

        State<TrackableBehaviour.Status, Action> _vuforiaState = new State<TrackableBehaviour.Status, Action>();
        State<PLACEMENT, Action> _interactionState = new State<PLACEMENT, Action>();

        bool SurfaceIndicatorVisibilityConditionsMet
        {
            get
            {
                return
                    (TrackingStatusIsTrackedOrLimited &&
                     GroundPlaneHitReceived && _interactionState.state == PLACEMENT.PLACING && Input.touchCount == 0);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
            VuforiaARController.Instance.RegisterOnPauseCallback(OnVuforiaPaused);
            DeviceTrackerARController.Instance.RegisterTrackerStartedCallback(OnTrackerStarted);
            DeviceTrackerARController.Instance.RegisterDevicePoseStatusChangedCallback(OnDevicePoseStatusChanged);
            this.planeFinder.HitTestMode = HitTestMode.AUTOMATIC;
            this.timer = new Timer(10000);
            this.timer.Elapsed += TimerFinished;
            this.timer.AutoReset = false;
            baseGrounds.SetActive(false);

            _vuforiaState.RegisterEvent(TrackableBehaviour.Status.TRACKED, () =>
            {

            });
            //OnARPlacingStatusChange

            _interactionState.RegisterEvent(PLACEMENT.NOTRACKING, () =>
            {
                Debug.Log("Placing...");
            });

            _interactionState.RegisterEvent(PLACEMENT.NORMAL, () =>
            {
                Debug.Log("NORMAL...");

            });


            _interactionState.RegisterEvent(PLACEMENT.PLACING, () =>
            {
                Debug.Log("PLACING...");

            });

            _interactionState.state = PLACEMENT.NOTRACKING;

        }

        void Update()
        {
            // The timer runs on a separate thread and we need to ResetTrackers on the main thread.
            if (this.timerFinished)
            {
                ResetTrackers();
                this.timerFinished = false;
            }
        }

        void LateUpdate()
        {
            GroundPlaneHitReceived = (this.automaticHitTestFrameCount == Time.frameCount);
            SetSurfaceIndicatorVisible(SurfaceIndicatorVisibilityConditionsMet);
        }

        void TimerFinished(System.Object source, ElapsedEventArgs e)
        {
            this.timerFinished = true;
        }

        void OnDestroy()
        {
            Debug.Log("OnDestroy() called.");

            VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
            VuforiaARController.Instance.UnregisterOnPauseCallback(OnVuforiaPaused);
            DeviceTrackerARController.Instance.UnregisterTrackerStartedCallback(OnTrackerStarted);
            DeviceTrackerARController.Instance.UnregisterDevicePoseStatusChangedCallback(OnDevicePoseStatusChanged);
        }

        void SetSurfaceIndicatorVisible(bool isVisible)
        {

            Renderer[] renderers = this.planeFinder.PlaneIndicator.GetComponentsInChildren<Renderer>(true);
            Canvas[] canvas = this.planeFinder.PlaneIndicator.GetComponentsInChildren<Canvas>(true);

            foreach (Canvas c in canvas)
                c.enabled = isVisible;

            foreach (Renderer r in renderers)
                r.enabled = isVisible;
        }

        void OnDevicePoseStatusChanged(TrackableBehaviour.Status status, TrackableBehaviour.StatusInfo statusInfo)
        {
            Debug.Log("PlaneManager.OnDevicePoseStatusChanged(" + status + ", " + statusInfo + ")");

            StatusCached = status;
            StatusInfoCached = statusInfo;
            _vuforiaState.state = StatusCached;

            CancelInvoke(nameof(ProcessCurrentStatus));
            if (!TrackingStatusIsTrackedAndNormal)
            {
                ProcessCurrentStatus();
            }
            else
            {
                Invoke(nameof(ProcessCurrentStatus), 4f);
            }

            // If the timer is running and the status is no longer Relocalizing, then stop the timer
            if (statusInfo != TrackableBehaviour.StatusInfo.RELOCALIZING && this.timer.Enabled)
            {
                this.timer.Stop();
            }

            switch (statusInfo)
            {
                case TrackableBehaviour.StatusInfo.NORMAL:
                    break;
                case TrackableBehaviour.StatusInfo.UNKNOWN:
                    break;
                case TrackableBehaviour.StatusInfo.INITIALIZING:
                    break;
                case TrackableBehaviour.StatusInfo.EXCESSIVE_MOTION:
                    break;
                case TrackableBehaviour.StatusInfo.INSUFFICIENT_FEATURES:
                    break;
                case TrackableBehaviour.StatusInfo.INSUFFICIENT_LIGHT:
                    break;
                case TrackableBehaviour.StatusInfo.RELOCALIZING:
                    // Start a 10 second timer to Reset Device Tracker
                    if (!this.timer.Enabled)
                    {
                        this.timer.Start();
                    }
                    break;
                default:
                    break;
            }
        }

        void ProcessCurrentStatus()
        {
            OnStatusChange?.Invoke(TrackingStatusIsTrackedAndNormal);
            baseGrounds.SetActive(TrackingStatusIsTrackedAndNormal);
        }

        void OnTrackerStarted()
        {
            Debug.Log("PlaneManager.OnTrackerStarted() called.");

            positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
            smartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();

            if (this.positionalDeviceTracker != null && this.smartTerrain != null)
            {
                if (!this.positionalDeviceTracker.IsActive)
                    this.positionalDeviceTracker.Start();

                if (!this.smartTerrain.IsActive)
                    this.smartTerrain.Start();

                Debug.Log("PositionalDeviceTracker is Active?: " + this.positionalDeviceTracker.IsActive +
                          "\nSmartTerrain Tracker is Active?: " + this.smartTerrain.IsActive);
            }
        }

        void OnVuforiaStarted()
        {
            Debug.Log("OnVuforiaStarted() called.");

            // Check trackers to see if started and start if necessary
            this.positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
            this.smartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();

            if (this.positionalDeviceTracker != null && this.smartTerrain != null)
            {
                if (!this.positionalDeviceTracker.IsActive)
                    this.positionalDeviceTracker.Start();
                if (this.positionalDeviceTracker.IsActive && !this.smartTerrain.IsActive)
                    this.smartTerrain.Start();

                OnStartARGroundPlane?.Invoke(true);
            }
            else
            {
                if (this.positionalDeviceTracker == null)
                    Debug.Log("PositionalDeviceTracker returned null. GroundPlane not supported on this device.");
                if (this.smartTerrain == null)
                    Debug.Log("SmartTerrain returned null. GroundPlane not supported on this device.");
                OnStartARGroundPlane?.Invoke(false);
                Debug.LogError("Unsupported Device");
            }
        }

        void OnVuforiaPaused(bool paused)
        {
            Debug.Log("OnVuforiaPaused(" + paused.ToString() + ") called.");

            if (paused)
                ResetScene();
        }

        void ResetScene()
        {
            Debug.Log("ResetScene() called.");

            // reset augmentations
        }

        void ResetTrackers()
        {
            Debug.Log("ResetTrackers() called.");

            this.smartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();
            this.positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();

            // Stop and restart trackers
            this.smartTerrain.Stop(); // stop SmartTerrain tracker before PositionalDeviceTracker
            this.positionalDeviceTracker.Reset();
            this.smartTerrain.Start(); // start SmartTerrain tracker after PositionalDeviceTracker
        }

        public void HandleAutomaticHitTest(HitTestResult result)
        {
            this.automaticHitTestFrameCount = Time.frameCount;

            if (_interactionState.state == PLACEMENT.PLACING)
            {
                if (contentPositioningBehaviour.AnchorStage != null)
                {
                    this.contentPositioningBehaviour.PositionContentAtPlaneAnchor(result);
                }
            }
        }

        public void HandleInteractiveHitTest(HitTestResult result)
        {
            //    if (result == null)
            //    {
            //        Debug.LogError("Invalid hit test result!");
            //        return;
            //    }

            //    if (!IsCanvasButtonPressed())
            //    {
            //        Debug.Log("HandleInteractiveHitTest() called.");

            //        //// Place object based on Ground Plane mode
            //        //switch (_interactionState.state)
            //        //{
            //        //    case PLACEMENT.PLACING:

            //        //        // With each tap, the Astronaut is moved to the position of the
            //        //        // newly created anchor. Before we set any anchor, we first want
            //        //        // to verify that the Status=TRACKED/EXTENDED_TRACKED and StatusInfo=NORMAL.
            //        //        if (TrackingStatusIsTrackedOrLimited)
            //        //        {
            //        //            this.contentPositioningBehaviour.AnchorStage = this.groundStage;
            //        //            this.contentPositioningBehaviour.PositionContentAtPlaneAnchor(result);
            //        //            this.groundStage.transform.localPosition = Vector3.zero;
            //        //        }

            //        //        break;
            //        //}
            //    }
        }

        //public bool IsCanvasButtonPressed()
        //{
        //    var pointerEventData = new PointerEventData(this.eventSystem)
        //    {
        //        position = Input.mousePosition
        //    };
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    this.graphicRayCaster.Raycast(pointerEventData, results);

        //    bool resultIsButton = false;
        //    foreach (RaycastResult result in results)
        //    {
        //        if (result.gameObject.GetComponentInParent<Toggle>() ||
        //            result.gameObject.GetComponent<Button>())
        //        {
        //            resultIsButton = true;
        //            break;
        //        }
        //    }
        //    return resultIsButton;
        //}

        //MVCC

        public override bool OnNotification(object p_event_path, UnityEngine.Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.VUFORIA_START_PLACEMENT:

                    var prefab = Resources.Load<POC>("objs/" + p_data[0].ToString());

                    var temp = Instantiate(prefab, baseGrounds.transform) as POC;
                    app.model.GetModel<VuforiaStateModel>().currentSelection = temp;

                    this.contentPositioningBehaviour.AnchorStage = temp.stage;

                    temp.StartPlacement();

                    _interactionState.state = PLACEMENT.PLACING;

                    break;

                case NOTIFYVUFORIA.VUFORIA_PLACE_ON_GROUND:

                    _interactionState.state = PLACEMENT.NORMAL;
                    app.model.GetModel<VuforiaStateModel>().currentSelection.PlaceReal();

                    break;
                case NOTIFYVUFORIA.VUFORIA_CANCEL_PLACEMENT:

                    if (app.model.GetModel<VuforiaStateModel>().currentSelection != null)
                    {
                        if (!app.model.GetModel<VuforiaStateModel>().currentSelection.IsPlaced)
                        {
                            Destroy(app.model.GetModel<VuforiaStateModel>().currentSelection.gameObject);
                            app.model.GetModel<VuforiaStateModel>().currentSelection = null;
                        }
                    }

                    break;

            }
            return true;
        }
    }
}