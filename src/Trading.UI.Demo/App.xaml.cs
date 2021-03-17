﻿using MahApps.Metro.Controls.Dialogs;
using OpenAPI.Net;
using OpenAPI.Net.Helpers;
using Prism.Ioc;
using System;
using System.Windows;
using Trading.UI.Demo.Helpers;
using Trading.UI.Demo.Services;
using Trading.UI.Demo.Views;

namespace Trading.UI.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();

            containerRegistry.RegisterForNavigation<OrdersView>();
            containerRegistry.RegisterForNavigation<CreateOrderView>();

            containerRegistry.RegisterDialog<ApiConfigurationView>();

            containerRegistry.RegisterDialogWindow<DialogWindow>();

            IOpenClient liveClientFactory() => new OpenClient(ApiInfo.LiveHost, ApiInfo.Port, TimeSpan.FromSeconds(10));
            IOpenClient demoClientFactory() => new OpenClient(ApiInfo.DemoHost, ApiInfo.Port, TimeSpan.FromSeconds(10));

            var apiService = new ApiService(liveClientFactory, demoClientFactory);

            containerRegistry.RegisterInstance<IApiService>(apiService);
        }
    }
}