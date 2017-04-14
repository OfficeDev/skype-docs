namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    // We need this interface so that we can hide the setter of Invitation.RelatedConversation
    internal interface IInvitationWithConversation
    {
        /// <summary>
        /// Sets the related conversation.
        /// </summary>
        /// <param name="conversation">The conversation.</param>
        void SetRelatedConversation(Conversation conversation);
    }
}
