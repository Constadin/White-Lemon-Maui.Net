using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WhiteLemonMauiUI.Helpers.Classes
{
    public class BaseMessagingCenter<T> : ValueChangedMessage<T> 
    {
        public BaseMessagingCenter(T value) : base(value) { }
    }
}
