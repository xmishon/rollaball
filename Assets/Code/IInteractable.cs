﻿namespace mzmeevskiy
{
    public interface IInteractable : IAction
    {
        bool IsInteractable { get; }
    }
}