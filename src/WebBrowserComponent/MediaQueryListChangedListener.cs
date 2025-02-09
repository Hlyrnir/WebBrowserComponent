using Microsoft.JSInterop;

namespace WebBrowserComponent
{
    public class MediaQueryListChangedListener : IAsyncDisposable
    {
        internal DotNetObjectReference<MediaQueryListChangedListener>? Reference;
        private const string sMethodName = "NotifyMediaQueryListChanged";

        private readonly IJSRuntime jsRuntime;
        private readonly string sQuery;

        public event EventHandler<MediaQueryListChangedArgs>? MediaQueryListChanged;

        private MediaQueryListChangedListener(string sQuery, IJSRuntime jsRuntime)
        {
            this.sQuery = sQuery;
            this.jsRuntime = jsRuntime;

            Reference = DotNetObjectReference.Create(this);
        }

        internal static async ValueTask<MediaQueryListChangedListener?> InitializeAsync(string sQuery, IJSRuntime jsRuntime, CancellationToken tknCancellation)
        {
            if (tknCancellation.IsCancellationRequested)
                return null;

            MediaQueryListChangedListener wbcMediaQueryListEventListener = new MediaQueryListChangedListener(sQuery, jsRuntime);

            if (wbcMediaQueryListEventListener.Reference is null)
                return null;

            await jsRuntime.InvokeVoidAsync("addMediaQueryListChangedListener", tknCancellation, sQuery, sMethodName, wbcMediaQueryListEventListener.Reference);
            
            return wbcMediaQueryListEventListener;
        }

        [JSInvokable]
        public void NotifyMediaQueryListChanged(bool bIsMatch)
        {
            OnMediaQueryListChangedEvent(new MediaQueryListChangedArgs { IsMatch = bIsMatch });
        }

        private void OnMediaQueryListChangedEvent(MediaQueryListChangedArgs evntLocalizationState)
        {
            EventHandler<MediaQueryListChangedArgs>? evntHandler = MediaQueryListChanged;

            if (evntHandler is not null)
                evntHandler(this, evntLocalizationState);
        }

        public async ValueTask DisposeAsync()
        {
            if (Reference is not null)
                await jsRuntime.InvokeVoidAsync("removeMediaQueryListChangedListener", CancellationToken.None, sQuery, sMethodName, Reference);
            
            if (Reference is not null)
                Reference.Dispose();
        }
    }
}