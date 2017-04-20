using System;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class Prompt : BasePlatformResource<PromptResource, PromptCapability>, IPrompt
    {
        #region Constructor

        internal Prompt(IRestfulClient restfulClient, PromptResource resource, Uri baseUri, Uri resourceUri, AudioVideoFlow parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "AudioVideoFlow resource is required");
            }
        }

        #endregion

        #region Public methods

         /// <summary>
        /// <see cref="Prompt"/> doesn't support any capability so always returns <code>false</code>.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>false</code> </returns>
        public override bool Supports(PromptCapability capability)
        {
            return false;
        }

        #endregion
    }
}
