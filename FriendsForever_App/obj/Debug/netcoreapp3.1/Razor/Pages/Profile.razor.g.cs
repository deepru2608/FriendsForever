#pragma checksum "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39b71f418d9669733a6b19dd4c07eb22117a1309"
// <auto-generated/>
#pragma warning disable 1591
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
    public partial class Profile : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "w3-card w3-round w3-white");
            __builder.AddMarkupContent(2, "\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "w3-container");
            __builder.AddMarkupContent(5, "\r\n        ");
            __builder.OpenElement(6, "h4");
            __builder.AddAttribute(7, "class", "w3-center mt-1 text-primary");
            __builder.AddAttribute(8, "style", "padding-top: 5px;");
            __builder.AddContent(9, 
#nullable restore
#line 4 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                                                           model.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\r\n        ");
            __builder.OpenElement(11, "p");
            __builder.AddAttribute(12, "class", "w3-center");
            __builder.AddMarkupContent(13, "\r\n");
#nullable restore
#line 6 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
              
                var profilePhotoPath = (model.ProfilePhotoPath ?? "Microsoft_Default.png");
            

#line default
#line hidden
#nullable disable
            __builder.AddContent(14, "            ");
            __builder.OpenElement(15, "img");
            __builder.AddAttribute(16, "src", "/Images/ProfileImage/" + (
#nullable restore
#line 9 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                            profilePhotoPath

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "class", "w3-circle");
            __builder.AddAttribute(18, "style", "height:106px;width:106px");
            __builder.AddAttribute(19, "alt", "Deepanshu");
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n        ");
            __builder.AddMarkupContent(22, "<p class=\"w3-center\">\r\n            <a href=\"/Home/ViewProfile\" class=\"btn btn-sm btn-primary\" style=\"color: white; list-style: none\">View Profile</a>\r\n        </p>\r\n        <hr>\r\n        ");
            __builder.OpenElement(23, "p");
            __builder.AddMarkupContent(24, "<i class=\"fas fa-envelope-open-text fa-fw w3-margin-right w3-text-theme\"></i> ");
            __builder.AddContent(25, 
#nullable restore
#line 15 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                                                                          model.Email

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n        ");
            __builder.OpenElement(27, "p");
            __builder.AddMarkupContent(28, "<i class=\"fa fa-home fa-fw w3-margin-right w3-text-theme\"></i> ");
            __builder.AddContent(29, 
#nullable restore
#line 16 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                                                           model.Country

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n        ");
            __builder.OpenElement(31, "p");
            __builder.AddMarkupContent(32, "<i class=\"fa fa-birthday-cake fa-fw w3-margin-right w3-text-theme\"></i> ");
            __builder.AddContent(33, 
#nullable restore
#line 17 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                                                                    model.Dob.ToLongDateString()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n<br>\r\n\r\n\r\n");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "class", "w3-card w3-round w3-white w3-hide-small");
            __builder.AddMarkupContent(39, "\r\n    ");
            __builder.OpenElement(40, "div");
            __builder.AddAttribute(41, "class", "w3-container");
            __builder.AddMarkupContent(42, "\r\n        ");
            __builder.AddMarkupContent(43, "<p>Interests</p>\r\n        ");
            __builder.OpenElement(44, "p");
            __builder.AddMarkupContent(45, "\r\n");
#nullable restore
#line 27 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
             if (model.UserInterests.Any())
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                 foreach (var userInterest in model.UserInterests)
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(46, "                    ");
            __builder.OpenElement(47, "span");
            __builder.AddAttribute(48, "class", "w3-tag w3-small w3-theme");
            __builder.AddContent(49, 
#nullable restore
#line 31 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                                                            userInterest

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n");
#nullable restore
#line 32 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
                 
            }
            else
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(51, "                ");
            __builder.AddMarkupContent(52, "<span class=\"w3-tag w3-small w3-theme\">No interest added.</span>\r\n");
#nullable restore
#line 37 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(53, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(55, "\r\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n<br>");
        }
        #pragma warning restore 1998
#nullable restore
#line 43 "C:\Users\deepa\Desktop\Asp Net Core\Social Network\FriendsForever\FriendsForever\FriendsForever_App\Pages\Profile.razor"
       
    public UserDashboardViewModel model = new UserDashboardViewModel();
    private IDataProtector dataProtector;

    private async Task<ApplicationUser> GetUser()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return await userManager.GetUserAsync(user);
    }
    protected override async Task OnInitializedAsync()
    {
        dataProtector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.RouteValue);
        var user = await GetUser();
        if (user != null)
        {
            model.Name = user.FullName;
            model.Dob = user.Dob;
            model.Country = user.Country;
            model.Email = user.Email;
            model.ProfilePhotoPath = user.ProfilePhotoPath;
            IEnumerable<string> interests = await centerRepository.FindByIdUserInterestAsync(user.Id);
            model.UserInterests = interests;
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