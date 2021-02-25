#pragma checksum "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\InstantStory.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "795206496a7740f74db58a82d0e4dedeb5577f45"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace FriendsForever_App.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using FriendsForever_App.Repository;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using FriendsForever_App.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using FriendsForever_App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.AspNetCore.DataProtection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using FriendsForever_App.Security;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\_Imports.razor"
using Microsoft.AspNetCore.ProtectedBrowserStorage;

#line default
#line hidden
#nullable disable
    public partial class InstantStory : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 16 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\InstantStory.razor"
       
    public string YourStory { get; set; }
    public string Name { get; set; }

    private async Task<ApplicationUser> GetUser()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return await userManager.GetUserAsync(user);
    }

    protected override async Task OnInitializedAsync()
    {
        var user = await GetUser();
        Name = user.FullName;
    }

    void NewPost()
    {
        navigationManager.NavigateTo("Home/NewPost", true);
    }

    async Task NewStory()
    {
        if (string.IsNullOrEmpty(YourStory))
        {
            await jS.ShowAlert("Invalid Request!");
        }
        else
        {
            var user = await GetUser();
            if (user != null)
            {
                string postId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                Post post = new Post
                {
                    PostedId = postId,
                    PostedOwnerName = user.FullName,
                    PostedOwnerPhoto = user.ProfilePhotoPath,
                    PostedOwnerUserId = user.Id,
                    PostedContent = YourStory,
                    PhotoAttached = "Not Attached",
                    PostedTimeStamp = DateTime.Now
                };
                var result = await centerRepository.InsertNewPostAsync(post);
                if (result > 0)
                {
                    StateHasChanged();
                }
                else
                {
                    await jS.ShowAlert("Invalid request, please try after sometimes!");
                }
            }
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ProtectedSessionStorage sessionStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime jS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserManager<ApplicationUser> userManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private DataProtectionPurposeStrings dataProtectionPurposeStrings { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IDataProtectionProvider dataProtectionProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAccountRepository accountRepository { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ICenterRepository centerRepository { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591
