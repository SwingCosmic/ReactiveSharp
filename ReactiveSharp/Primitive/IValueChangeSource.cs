﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveSharp.Primitive;

public class ValueChangedEventArgs(object? newValue, object? oldValue) : EventArgs
{
    public object? OldValue { get; } = oldValue;
    public object? NewValue { get; } = newValue;
}

public interface IValueChangeSource
{
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;
}