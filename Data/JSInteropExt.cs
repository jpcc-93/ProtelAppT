using Microsoft.JSInterop;

namespace ProtelAppT.Data
{
    public static class JSInteropExt
    {
        public static async Task SaveAsFileAsync(this IJSRuntime js, string fileName, byte[] data, string type="application/octet-stream")
        {
            await js.InvokeAsync<object>("JSInteropExt.saveAsFile", fileName, type, Convert.ToBase64String(data));
        }

    }
}
