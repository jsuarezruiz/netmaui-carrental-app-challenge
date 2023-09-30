// Copyright © Plain Concepts S.L.U. All rights reserved. Use is subject to license terms.

using Evergine.Common.Input;
using Evergine.Common.Input.Mouse;
using Evergine.Common.Input.Pointer;
using Evergine.Framework;
using Evergine.Framework.Graphics;
using Evergine.Mathematics;
using System;
using System.Drawing;
using System.Linq;

namespace CarRentalApp.Behaviors
{
    public class CameraBehavior : Behavior
    {
        /// <summary>
        /// The camera to move.
        /// </summary>
        [BindComponent(false)]
        public Transform3D Transform = null;

        /// <summary>
        /// The camera transform.
        /// </summary>
        private Transform3D cameraTransform;

        /// <summary>
        /// True if the camera Is Dragging.
        /// </summary>
        private bool isRotating;

        /// <summary>
        /// The orbit_scale.
        /// </summary>
        private const float OrbitScale = 0.005f;

        /// <summary>
        /// Gets or sets the touch sensibility.
        /// </summary>
        /// <remarks>
        /// 0.5 is for stop, 1 is for raw delta, 2 is twice delta.
        /// </remarks>
        public float TouchSensibility { get; set; }

        /// <summary>
        /// The theta.
        /// </summary>
        private float theta;

        /// <summary>
        /// The is dirty.
        /// </summary>
        private bool isDirty;

        /// <summary>
        /// The current mouse state.
        /// </summary>
        private Vector3 cameraInitialPosition;

        private MouseDispatcher mouseDispatcher;
        private Evergine.Mathematics.Point currentMouseState;
        private Vector2 lastMousePosition;

        private PointerDispatcher touchDispatcher;
        private Evergine.Mathematics.Point currentTouchState;
        private Vector2 lastTouchPosition;

        public CameraBehavior()
        {
        }

        /// <inheritdoc/>
        protected override void OnLoaded()
        {
            base.OnLoaded();

            this.theta = 0;

            this.isRotating = false;

            this.isDirty = true;
        }

        /// <inheritdoc/>
        protected override bool OnAttached()
        {
            this.cameraTransform = this.Owner.ChildEntities.First().FindComponent<Transform3D>();

            this.cameraInitialPosition = this.cameraTransform.LocalPosition;

            return base.OnAttached();
        }

        /// <inheritdoc/>
        protected override void OnActivated()
        {
            base.OnActivated();

            var display = this.Owner.Scene.Managers.RenderManager.ActiveCamera3D?.Display;
            if (display != null)
            {
                this.mouseDispatcher = display.MouseDispatcher;
                this.touchDispatcher = display.TouchDispatcher;
            }
        }

        /// <inheritdoc/>
        protected override void Update(TimeSpan gameTime)
        {
            this.HandleInput();

            if (this.isDirty)
            {
                this.CommitChanges();
                this.isDirty = false;
            }
        }

        private void HandleInput()
        {
            if (Evergine.Platform.DeviceInfo.PlatformType == Evergine.Common.PlatformType.Windows)
            {
                this.HandleMouse();
            }
            else
            {
                this.HandleTouch();
            }
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        private void HandleMouse()
        {
            if (this.mouseDispatcher == null)
            {
                return;
            }

            // Orbit            
            if (this.mouseDispatcher.IsButtonDown(MouseButtons.Left))
            {
                this.currentMouseState = this.mouseDispatcher.Position;

                if (this.isRotating == false)
                {
                    this.isRotating = true;
                }
                else
                {
                    Vector2 delta = Vector2.Zero;

                    delta.X = -this.currentMouseState.X + this.lastMousePosition.X;
                    delta.Y = this.currentMouseState.Y - this.lastMousePosition.Y;

                    delta = -delta;
                    this.Orbit(delta * OrbitScale);
                }

                this.lastMousePosition.X = this.currentMouseState.X;
                this.lastMousePosition.Y = this.currentMouseState.Y;
            }
            else
            {
                this.isRotating = false;
            }
        }

        private void HandleTouch()
        {
            if (this.touchDispatcher == null)
            {
                return;
            }

            var point = this.touchDispatcher.Points.FirstOrDefault();            
            
            if (point == null)
            {
                return;
            }

            if (point.State == ButtonState.Pressed)
            {                
                this.currentTouchState = point.Position;

                if (this.isRotating == false)
                {
                    this.isRotating = true;
                }
                else
                {
                    Vector2 delta = Vector2.Zero;
                    delta.X = -this.currentTouchState.X + this.lastTouchPosition.X;
                    delta.Y = this.currentTouchState.Y - this.lastTouchPosition.Y;

                    delta = -delta;
                    this.Orbit(delta * OrbitScale);
                }

                this.lastTouchPosition.X = this.currentTouchState.X;
                this.lastTouchPosition.Y = this.currentTouchState.Y;
            }
            else
            {
                this.isRotating = false;
            }
        }

        /// <summary>
        /// Orbits the specified delta.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public void Orbit(Vector2 delta)
        {
            this.theta += delta.X;

            this.isDirty = true;
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public void CommitChanges()
        {
            var rotation = this.Transform.LocalRotation;
            rotation.Y = -this.theta;

            this.Transform.LocalRotation = rotation;
        }

        /// <summary>
        /// Reset camera position.
        /// </summary>
        public void Reset()
        {
            this.cameraTransform.LocalPosition = this.cameraInitialPosition;
            this.cameraTransform.LocalLookAt(Vector3.Zero, Vector3.Up);
            this.Transform.LocalPosition = Vector3.Zero;
            this.Transform.LocalRotation = Vector3.Zero;

            this.theta = 0;

            this.isRotating = false;
        }
    }
}
