// 代码生成时间: 2025-09-21 10:22:34
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationBlazorApp
{
    // 定义应用程序状态
    public class AppState
    {
        public bool IsUserLoggedIn { get; set; }
    }

    // 定义用户角色枚举
    public enum UserRole
    {
        Guest,
        User,
        Admin
    }

    // 定义授权服务
    public class AuthorizationService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthorizationService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> CheckUserAccessAsync(UserRole requiredRole)
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            return user.IsInRole(requiredRole.ToString());
        }
    }

    // 定义需要授权的组件
    public class AuthorizeViewComponent : ComponentBase
    {
        [Inject]
        private AuthorizationService AuthorizationService { get; set; }

        [Parameter]
        public UserRole RequiredRole { get; set; }

        [Parameter]
        public RenderFragment<ClaimsPrincipal> ChildContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var hasAccess = await AuthorizationService.CheckUserAccessAsync(RequiredRole);
            if (!hasAccess)
            {
                throw new UnauthorizedAccessException("User does not have access to this resource.");
            }
        }

        public RenderFragment Render()
        {
            return ChildContent;
        }
    }

    // 定义应用程序的主组件
    public class MainLayout : LayoutComponentBase
    {
        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public void OnLogoutClick()
        {
            AppState.IsUserLoggedIn = false;
            NavigationManager.NavigateTo("/");
        }

        public void OnLoginClick()
        {
            AppState.IsUserLoggedIn = true;
            NavigationManager.NavigateTo("/secure");
        }
    }
}
