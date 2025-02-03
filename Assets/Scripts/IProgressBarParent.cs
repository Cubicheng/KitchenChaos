using System;
using UnityEngine;

public interface IProgressBarParent{
    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;
    public class OnProgressBarChangedEventArgs : EventArgs {
        public float progress;
    }
}
