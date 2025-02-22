﻿using System;
using System.Threading;
using UnityEngine;
using Mondeto.Core;

namespace Mondeto
{

public class UnityLogOutput : MonoBehaviour
{
    public void Awake()
    {
        // Log output is done in main thread
        //  https://qiita.com/toRisouP/items/a2c1bb1b0c4f73366bc6
        SynchronizationContext context = SynchronizationContext.Current;
        
        // If batch mode, log is only written into stdout (Console.WriteLine).
        // If graphics is available, log is written into Unity debug console.
        if (!Application.isBatchMode)
        {
            Mondeto.Core.Logger.OnLog += (type, component, msg) => {
                context.Post(_ => Debug.Log($"[{Mondeto.Core.Logger.LogTypeToString(type)}] {component}: {msg}"), null);
            };
        }
        else
        {
            Mondeto.Core.Logger.OnLog += (type, component, msg) => {
                context.Post(_ => Console.WriteLine($"[{Mondeto.Core.Logger.LogTypeToString(type)}] {component}: {msg}"), null);
            };
        }
    }
}

}   // end namespace