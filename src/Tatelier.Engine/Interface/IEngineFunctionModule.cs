using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine.Interface
{
    public interface IEngineFunctionModule
    {
        string Title { get; set; }

        bool WindowMode { get; set; }

        int ModuleStart();

        int ModuleFinish();

        int ProcessMessage();

        int SetGraphMode(int width, int height, int colorBitDepth);

        int SetWindowSize(int width, int height);

        int ScreenFlip();

        int ClearDrawScreen();

        int LoadGraph(string FileName, int NotUse3DFlag = 0);

        int DrawGraph(int x, int y, int grHandle, int transFlag);

        int DrawGraphF(float xf, float yf, int grHandle, int transFlag);

        int CreateFont(string fontName, int size, int thick, int fontType);
    }
}
