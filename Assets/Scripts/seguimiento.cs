using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace MixedReality.Toolkit.SpatialManipulation{
    /**
     * Clase que representa un indicador direccional para la manipulación espacial.
     * Esta clase se encarga de mostrar un indicador visual para guiar al usuario hacia un objetivo
     * cuando este no está en la vista del usuario.
     */
    [AddComponentMenu("MRTK/Spatial Manipulation/Solvers/Directional Indicator")]
    public class DirectionalIndicator : Solver{
        [Tooltip("The GameObject transform to point the indicator towards when this object is not in view.\nThe frame of reference for viewing is defined by the Solver Handler Tracked Target Type")]
        public Transform DirectionalTarget;
        [Tooltip("The offset from center to place the indicator. If frame of reference is the camera, then the object will be this distance from center of screen")]
        [Min(0.0f)]
        public float MinIndicatorScale = 0.05f;
        [Tooltip("The offset from center to place the indicator. If frame of reference is the camera, then the object will be this distance from center of screen")]
        [Min(0.0f)]
        public float MaxIndicatorScale = 0.2f;
        [Tooltip("Multiplier factor to increase or decrease FOV range for testing if object is visible and thus turn off indicator")]
        [Min(0.1f)]
        public float VisibilityScaleFactor = 0.8f;
        [Tooltip("The offset from center to place the indicator. If frame of reference is the camera, then the object will be this distance from center of screen")]
        [Min(0.0f)]
        public float ViewOffset = 0.3f;
        private bool indicatorShown = false;
        private static List<Renderer> childRenderers = new List<Renderer>();

        protected override void Start(){
            base.Start();
            SetIndicatorVisibility(ShouldShowIndicator());
        }

        private static readonly ProfilerMarker UpdatePerfMarker =
            new ProfilerMarker("[MRTK] DirectionalIndicator.Update");

        private void Update(){
            using (UpdatePerfMarker.Auto()){
                bool showIndicator = ShouldShowIndicator();
                if (showIndicator != indicatorShown){
                    SetIndicatorVisibility(showIndicator);
                }
            }
        }

        private bool ShouldShowIndicator(){
            if (DirectionalTarget == null || SolverHandler.TransformTarget == null){
                return false;
            }
            return !MathUtilities.IsInFOV(DirectionalTarget.position, SolverHandler.TransformTarget,
                VisibilityScaleFactor * Camera.main.fieldOfView, VisibilityScaleFactor * Camera.main.GetHorizontalFieldOfViewDegrees(),
                Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }

        private void SetIndicatorVisibility(bool showIndicator){
            SolverHandler.UpdateSolvers = showIndicator;
            childRenderers.Clear();
            GetComponentsInChildren<Renderer>(childRenderers);
            foreach (var renderer in childRenderers){
                renderer.enabled = showIndicator;
            }
            indicatorShown = showIndicator;
        }

        private static readonly ProfilerMarker SolverUpdatePerfMarker =
            new ProfilerMarker("[MRTK] DirectionalIndicator.SolverUpdate");

        public override void SolverUpdate(){
            using (SolverUpdatePerfMarker.Auto()){
                if (DirectionalTarget == null){
                    return;
                }
                var solverReferenceFrame = SolverHandler.TransformTarget;
                Vector3 origin = solverReferenceFrame.position + solverReferenceFrame.forward;
                Vector3 trackerToTargetDirection = (DirectionalTarget.position - solverReferenceFrame.position).normalized;
                Vector3 indicatorDirection = Vector3.ProjectOnPlane(trackerToTargetDirection, -solverReferenceFrame.up).normalized;
                if (indicatorDirection == Vector3.zero){
                    indicatorDirection = solverReferenceFrame.right;
                }
                GoalPosition = origin + indicatorDirection * ViewOffset;
                GoalRotation = Quaternion.LookRotation(solverReferenceFrame.forward, indicatorDirection);
                float minVisibilityAngle = Camera.main.fieldOfView;
                float angleToVisibilityFOV = Vector3.Angle(trackerToTargetDirection - solverReferenceFrame.position, solverReferenceFrame.forward) - minVisibilityAngle;
                float visibilityScale = 180f - minVisibilityAngle;
                GoalScale = Vector3.one * Mathf.Lerp(MinIndicatorScale, MaxIndicatorScale, angleToVisibilityFOV / visibilityScale);
            }
        }
    }
}

