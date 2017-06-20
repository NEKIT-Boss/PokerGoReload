using System;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace Prism.MEF2.Services
{
    public interface IExtendedNavigationService : INavigationService
    {
        /// <summary>
        /// Navigates to the specified page type, passing optional parameter
        /// </summary>
        /// <param name="pageType">Type of the page to navigate to.</param>
        /// <param name="parameter">Parameter to be passed while navigating.</param>
        /// <returns>Returns <c>true</c> if navigation succeeds; <c>false</c> otherwise.</returns>
        bool Navigate(Type pageType, object parameter = null);

        /// <summary>
        /// Navigates to the specified page type, passing optional parameter
        /// </summary>
        /// <typeparam name="TPageType">The page type to naviagte to.</typeparam>
        /// <param name="parameter">Parameter to be passed while navigating.</param>
        /// <returns>Returns <c>true</c> if navigation succeeds; <c>false</c> otherwise.</returns>
        bool Navigate<TPageType>(object parameter = null);

        /// <summary>
        /// Workaround for MEF 2 instance export limitation. Providing the ability to defer the initialization of contained FrameNavigationService.
        /// </summary>
        /// <param name="frameFacade">The FrameFacade for the NavigationService.</param>
        /// <param name="navigationResolver">NavigationResolver for the NavigationService.</param>
        /// <param name="sessionStateService">The SessionStateService for the NavigationService.</param>
        void Initialize(IFrameFacade frameFacade, Func<string, Type> navigationResolver, ISessionStateService sessionStateService);
    }
}