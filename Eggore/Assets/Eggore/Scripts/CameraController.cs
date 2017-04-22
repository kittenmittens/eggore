﻿namespace Eggore
{
    using UnityEngine;

    /// <summary>
    /// Handles user input for a player-controlled camera.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {

        protected const int MOUSE_BUTTON = 0;
        protected const string MOUSE_AXIS_X = "Mouse X";
        protected const string MOUSE_AXIS_Y = "Mouse Y";
        protected const string MOUSE_SCROLL = "Mouse ScrollWheel";

        public PlayerController target;
        public float distance = 5F;
        public float distanceMin = 1F;
        public float distanceMax = 10F;
        public float followSpeed = 3F;
        public float pitchMin = -20F;
        public float pitchMax = 80F;
        public float scrollSpeed = 1F;
        public float xSpeed = 1F;
        public float ySpeed = 1F;

        protected bool follow;
        protected float pitch;
        protected float yaw;

        private new Camera camera;

        protected void Awake()
        {
            if (camera == null)
            {
                camera = GetComponent<Camera>();
            }

            if (target == null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    target = player.GetComponent<PlayerController>();
                }
            }

            pitch = camera.transform.rotation.eulerAngles[0];
            yaw = camera.transform.rotation.eulerAngles[1];
        }

        protected float ClampAngle(float angle)
        {
            angle = angle < -360F ? angle + 360F : angle;
            angle = angle > 360F ? angle - 360F : angle;
            return Mathf.Clamp(angle, pitchMin, pitchMax);
        }

        protected void LateUpdate()
        {
            if (target)
            {
                if (Input.GetMouseButton(MOUSE_BUTTON))
                {
                    yaw += Input.GetAxis(MOUSE_AXIS_X) * xSpeed * distance;
                    pitch -= Input.GetAxis(MOUSE_AXIS_Y) * ySpeed;
                    pitch = ClampAngle(pitch);
                    camera.transform.rotation = Quaternion.Euler(pitch, yaw, 0F);
                }
                else
                {
                    yaw = Mathf.LerpAngle(yaw, target.Focus.eulerAngles.y, followSpeed * Time.deltaTime);
                    camera.transform.rotation = Quaternion.Euler(pitch, yaw, 0F);
                }

                distance = Mathf.Clamp(distance - Input.GetAxis(MOUSE_SCROLL) * scrollSpeed, distanceMin, distanceMax);
                camera.transform.position = camera.transform.rotation * Vector3.back * distance + target.Focus.position;
            }
        }

    }

}