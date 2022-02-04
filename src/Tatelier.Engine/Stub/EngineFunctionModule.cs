﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Engine.Stub
{
    public class EngineFunctionModule
        : IEngineFunctionModule
    {
        SHA256 sha256 = SHA256.Create();

        public string Title { get; set; }

        public bool WindowMode { get; set; } = false;

        public int ClearDrawScreen()
        {
            return 0;
        }

        public int CreateFont(string fontName, int size, int thick, int fontType)
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(fontName));

            int handle = BitConverter.ToInt32(bytes, 0);

            return handle;
        }

        public int DrawGraph(int x, int y, int grHandle, int transFlag)
        {
            return 0;
        }

        public int DrawGraphF(float xf, float yf, int grHandle, int transFlag)
        {
            return 0;
        }

        public int ModuleFinish()
        {
            return 0;
        }

        public int ModuleStart()
        {
            return 0;
        }

        public int LoadGraph(string filePath, int notUse3DFlag = 0)
        {
            var bytes = sha256.ComputeHash(File.OpenRead(filePath));

            int handle = BitConverter.ToInt32(bytes, 0);

            return handle;
        }

        public int ProcessMessage()
        {
            return 0;
        }

        public int ScreenFlip()
        {
            return 0;
        }

        public int SetGraphMode(int width, int height, int colorBitDepth)
        {
            return 0;
        }

        public int SetWindowSize(int width, int height)
        {
            return 0;
        }
    }
}
