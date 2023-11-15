using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace In_Class_Problem_To_Do_List_DB
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}