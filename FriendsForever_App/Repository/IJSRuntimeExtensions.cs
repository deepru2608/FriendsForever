using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask ShowAlert(this IJSRuntime jS, string message)
        {
            return jS.InvokeVoidAsync("Swal.fire", message);
        }

        public static ValueTask ShowAlert(this IJSRuntime js, string title, string message, SweetAlertMessageType sweetAlertMessageType)
        {
            return js.InvokeVoidAsync("Swal.fire", title, message, sweetAlertMessageType.ToString());
        }

        public static ValueTask ShowModel(this IJSRuntime jS, string ModelId)
        {
            return jS.InvokeVoidAsync("global.openModal", ModelId);
        }

        public static ValueTask CloseModel(this IJSRuntime jS, string ModelId)
        {
            return jS.InvokeVoidAsync("global.closeModal", ModelId);
        }
    }

    public enum SweetAlertMessageType
    {
        question, warning, error, success, info
    }
}
