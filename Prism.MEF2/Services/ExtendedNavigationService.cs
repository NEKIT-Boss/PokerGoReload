using System;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Prism.Logging;

namespace Prism.MEF2.Services
{
    /// <summary>
    /// Defers the initalization of real NaviagationService to Initialize method.
    /// </summary>
    class ExtendedNavigationService : IExtendedNavigationService
    {
        /// <summary>
        /// The inner navigationService, to which interface implementation is delegated
        /// </summary>
        private INavigationService _navigationService;

        private readonly ILoggerFacade _loggerFacade;

        public ExtendedNavigationService(ILoggerFacade loggerFacade)
        {
            _loggerFacade = loggerFacade;
        }

        public void Initialize(IFrameFacade frameFacade, Func<string, Type> navigationResolver, ISessionStateService sessionStateService)
        {
            _loggerFacade.Log("Initializing navigation service", Category.Debug, Priority.Medium);

            if (frameFacade == null) throw new ArgumentNullException(nameof(frameFacade));
            if (navigationResolver == null) throw new ArgumentNullException(nameof(navigationResolver));
            if (sessionStateService == null) throw new ArgumentNullException(nameof(sessionStateService));
           
            _navigationService = new FrameNavigationService(frameFacade, navigationResolver, sessionStateService);
        }

        public bool Navigate(Type pageType, object parameter = null)
        {
            _loggerFacade.Log($"Requested navigate to {pageType}", Category.Info, Priority.Low);

            return _navigationService.Navigate(pageType.AssemblyQualifiedName, parameter);
        }

        public bool Navigate<TPageType>(object parameter = null)
        {
            return Navigate(typeof(TPageType), parameter);
        }

        #region DelegatingTheImplementation

        public bool Navigate(string pageToken, object parameter = null)
        {
            throw new NotImplementedException("Can not navigate by key in this NavigationService implementation!");
        }

        public void GoBack()
        {
            _loggerFacade.Log($"Navigation back requested", Category.Info, Priority.Low);

            _navigationService.GoBack();
        }

        public bool CanGoBack()
        {
            return _navigationService.CanGoBack();
        }

        public void GoForward()
        {
            _loggerFacade.Log($"Navigation forward requested", Category.Info, Priority.Low);

            _navigationService.GoForward();
        }

        public bool CanGoForward()
        {
            return _navigationService.CanGoForward();
        }

        public void ClearHistory()
        {
            _navigationService.ClearHistory();
        }

        public void RemoveFirstPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException("Can not remove by key in this NavigationService implementation!");
        }

        public void RemoveLastPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException("Can not remove by key in this NavigationService implementation!");
        }

        public void RemoveAllPages(string pageToken = null, object parameter = null)
        {
            _loggerFacade.Log($"Requested pages cleanup", Category.Debug, Priority.Low);

            _navigationService.RemoveAllPages(pageToken, parameter);
        }

        public void RestoreSavedNavigation()
        {
            _loggerFacade.Log($"Restoring navigation service", Category.Debug, Priority.Low);

            _navigationService.RestoreSavedNavigation();
        }

        public void Suspending()
        {
            _loggerFacade.Log($"Suspending navigation service", Category.Debug, Priority.Low);

            _navigationService.Suspending();
        }

        #endregion
    }
}
