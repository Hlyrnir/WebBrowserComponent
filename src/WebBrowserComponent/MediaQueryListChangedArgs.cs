using System;

namespace WebBrowserComponent
{
    public sealed class MediaQueryListChangedArgs : EventArgs
    {
        public required bool IsMatch { get; init; }
    }
}
