using System;
using Microsoft.Practices.Unity;

namespace QuickSamplesCommon
{
    /// <summary>
    /// Helper class for unity
    /// </summary>
    public static class UnityHelper
    {
        /// <summary>
        /// The default  Container.
        /// </summary>
        private static Lazy<IUnityContainer> defaultContainer = new Lazy<IUnityContainer>(() => new UnityContainer());

        /// <summary>
        /// Gets the default IOC Container.
        /// </summary>
        public static IUnityContainer DefaultContainer
        {
            get { return defaultContainer.Value; }
        }

        /// <summary>
        /// Resolve instance base the Generic type.
        /// </summary>
        /// <typeparam name="T">Any class or structure.</typeparam>
        /// <returns>The resolved instance.</returns>
        public static T Resolve<T>()
        {
            return DefaultContainer.Resolve<T>();
        }
    }
}
