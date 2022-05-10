using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
    class Program
    {
        static void Main(string[] args)
        {
        Mundo window = Mundo.GetInstance(600, 600);
        window.Title = "CG_N2";
        window.Run(1.0 / 60.0);
        } 
    }
}