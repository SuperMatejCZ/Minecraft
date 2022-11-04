﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    public static class Player
    {
        public static Matrix4 viewMatrix;
        public static Matrix4 projMatrix;
        public static Vector3 position = Vector3.Zero;
        private static Vector3 center = Vector3.Zero;
        private static readonly Vector3 up = Vector3.UnitY;
        public static bool ortho = false;
        public static Vector3 Rotation;

        private static float mod(float x, float m)
        {
            float r = x % m;
            return r < 0 ? r + m : r;
        }

        public static void SetRotation(Vector3 rot)
        {
            Rotation = rot;
        }

        public static void Move(float offset, float speed)
        {
            Vector3 move = new Vector3(0f, 0f, 1f) * speed;
            Matrix3 mat = Matrix3.CreateRotationY(((Rotation.Y + offset) * Util.PI) / 180f);
            position += move * mat;
        }

        public static void UpdateView(float width, float height)
        {
            Rotation.Y = mod(Rotation.Y, 360);
            float x = (Rotation.X * Util.PI) / 180f;
            float y = (Rotation.Y * Util.PI) / 180f;
            Vector3 offset = new Vector3(0f, 0f, 1f);
            Matrix3 mat = Matrix3.CreateRotationX(x) * Matrix3.CreateRotationY(y);
            center = position + (offset * mat);
            
            viewMatrix = Matrix4.LookAt(position, center, up);

            if (ortho) {
                float projWidth = width;
                float aspect = width / height;
                float projHeight = width / aspect;

                float left = -projWidth / 2f;
                float right = projWidth / 2f;
                float bottom = -projHeight / 2f;
                float top = projHeight / 2f;
                float near = 0.0001f;
                float far = 10000f;

                projMatrix = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, near, far);
            } else {
                float fov = 45f;
                float near = 0.0001f;
                float far = 10000f;
                float aspect = width / height;
                 projMatrix = Matrix4.CreatePerspectiveFieldOfView(DegToRad(fov), aspect, near, far);
            }
        }

        const float PI = (float)System.Math.PI;
        const float PIDiv = PI / 180f;

        static float DegToRad(float degrees)
        {
            float radians = PIDiv * degrees;
            return (radians);
        }
    }
}