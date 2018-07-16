﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Internal.Definitions.InputSystem;
using Microsoft.MixedReality.Toolkit.Internal.EventDatum.Input;
using Microsoft.MixedReality.Toolkit.Internal.Interfaces.InputSystem;
using Microsoft.MixedReality.Toolkit.Internal.Interfaces.InputSystem.Handlers;
using Microsoft.MixedReality.Toolkit.Internal.Managers;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.SDK.Input
{
    /// <summary>
    /// This component handles pointer clicks from all types of input sources.<para/>
    /// i.e. a primary mouse button click, motion controller selection press, or hand tap.
    /// </summary>
    public class PointerClickHandler : MonoBehaviour, IMixedRealityPointerHandler
    {
        private IMixedRealityInputSystem inputSystem = null;
        private IMixedRealityInputSystem InputSystem => inputSystem ?? (inputSystem = MixedRealityManager.Instance.GetManager<IMixedRealityInputSystem>());

        [SerializeField]
        [Tooltip("Is Gaze required for the action to raise the event?")]
        private bool isGazeRequired = false;

        [SerializeField]
        [Tooltip("The input actions to be recognized on pointer up.")]
        private InputActionEventPair onPointerUpActionEvents;

        [SerializeField]
        [Tooltip("The input actions to be recognized on pointer down.")]
        private InputActionEventPair onPointerDownActionEvents;

        [SerializeField]
        [Tooltip("The input actions to be recognized on pointer clicked.")]
        private InputActionEventPair onPointerClickedActionEvents;

        #region Monobehaviour Implementation

        private void OnEnable()
        {
            if (isGazeRequired)
            {
                InputSystem.Register(gameObject);
            }
        }

        private void OnDisable()
        {
            if (isGazeRequired)
            {
                InputSystem.Unregister(gameObject);
            }
        }

        #endregion Monobehaviour Implementation

        #region IMixedRealityPointerHandler Implementation

        /// <inheritdoc />
        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            if (onPointerUpActionEvents.InputAction == MixedRealityInputAction.None) { return; }

            if (onPointerUpActionEvents.InputAction == eventData.MixedRealityInputAction)
            {
                onPointerUpActionEvents.UnityEvent.Invoke();
            }
        }

        /// <inheritdoc />
        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            if (onPointerDownActionEvents.InputAction == MixedRealityInputAction.None) { return; }

            if (onPointerDownActionEvents.InputAction == eventData.MixedRealityInputAction)
            {
                onPointerDownActionEvents.UnityEvent.Invoke();
            }
        }

        /// <inheritdoc />
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            if (onPointerClickedActionEvents.InputAction == MixedRealityInputAction.None) { return; }

            if (onPointerClickedActionEvents.InputAction == eventData.MixedRealityInputAction)
            {
                onPointerClickedActionEvents.UnityEvent.Invoke();
            }
        }

        #endregion IMixedRealityPointerHandler Implementation
    }
}
