using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;

namespace WebBrowserComponent
{
    public static class JsRuntimeExtension
    {
        public static async ValueTask<bool> ShowAlertAsync(this IJSRuntime jsRuntime, string sMessage, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return false;

            await jsRuntime.InvokeVoidAsync("showAlert", tknCancellation, sMessage);

            return true;
        }

        public static async ValueTask<bool> ShowConfirmAsync(this IJSRuntime jsRuntime, string sMessage, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return false;

            return await jsRuntime.InvokeAsync<bool>("showConfirm", tknCancellation, sMessage);
        }

        public static async ValueTask<bool> ShowDialogAsync(this IJSRuntime jsRuntime, ElementReference elemReference, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return false;

            await jsRuntime.InvokeVoidAsync("showDialog", tknCancellation, elemReference);

            return true;
        }

        public static async ValueTask<bool> CloseDialogAsync(this IJSRuntime jsRuntime, ElementReference elemReference, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return false;

            await jsRuntime.InvokeVoidAsync("closeDialog", tknCancellation, elemReference);

            return true;
        }

        public static async ValueTask<string> ShowPromptAsync(this IJSRuntime jsRuntime, string sMessage, string? sDefaultValue = null, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return string.Empty;

            return await jsRuntime.InvokeAsync<string>("showPrompt", tknCancellation, sMessage, sDefaultValue);
        }

        public static async ValueTask<bool> CheckMediaQueryListMatch(this IJSRuntime jsRuntime, string sQuery, CancellationToken tknCancellation)
        {
            if (tknCancellation.IsCancellationRequested)
                return false;

            await jsRuntime.InvokeVoidAsync("checkMediaQueryListMatch", tknCancellation, sQuery);

            return true;
        }

        public static async ValueTask<MediaQueryListChangedListener?> CreateMediaQueryListChangedListener(this IJSRuntime jsRuntime, string sQuery, CancellationToken tknCancellation = default)
        {
            if (tknCancellation.IsCancellationRequested)
                return null;

            return await MediaQueryListChangedListener.InitializeAsync(sQuery, jsRuntime, tknCancellation);
        }
    }
}