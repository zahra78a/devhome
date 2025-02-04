﻿// Copyright (c) Microsoft Corporation and Contributors
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace DevHome.SetupFlow.Common.WindowsPackageManager;

internal sealed class ClassModel
{
    /// <summary>
    /// Gets the interface for the projected class type generated by CsWinRT
    /// </summary>
    public Type InterfaceType { init; get; }

    /// <summary>
    /// Gets the projected class type generated by CsWinRT
    /// </summary>
    public Type ProjectedClassType { init;  get; }

    /// <summary>
    /// Gets the clsids for each context (e.g. OutOfProcProd, OutOfProcDev)
    /// </summary>
    public IReadOnlyDictionary<ClsidContext, Guid> Clsids { init; get; }

    /// <summary>
    /// Get CLSID based on the provided context
    /// </summary>
    /// <param name="context">Context</param>
    /// <returns>CLSID for the provided context.</returns>
    /// <exception cref="InvalidOperationException">Throw an exception if the clsid context is not available for the current instance.</exception>
    public Guid GetClsid(ClsidContext context)
    {
        if (!Clsids.TryGetValue(context, out var clsid))
        {
            throw new InvalidOperationException($"{ProjectedClassType.FullName} is not implemented in context {context}");
        }

        return clsid;
    }

    /// <summary>
    /// Get IID corresponding to the COM object
    /// </summary>
    /// <returns>IID.</returns>
    public Guid GetIid()
    {
        return InterfaceType.GUID;
    }
}
