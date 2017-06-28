using ATZ.MVVM.ViewModels.Utility;
using System.ComponentModel;

namespace ATZ.ObservableObject.Tests
{
    public class TestObservableObject : ObservableObject
    {
        private int _a;
        private int _propertyRaisingChangeNotification;

        public int A
        {
            get => _a;
            set => Set(ref _a, value, new[] { "B" });
        }

        public int PropertyRaisingChangeNotification
        {
            get => _propertyRaisingChangeNotification;
            set => Set(ref _propertyRaisingChangeNotification, value);
        }

        public void NullAdditionalProperties()
        {
            Set(ref _propertyRaisingChangeNotification, _propertyRaisingChangeNotification + 1);
        }

        public void SetWith2Parameters(int value)
        {
            Set(ref _propertyRaisingChangeNotification, value);
        }

        public new SuspendPropertyChangedEvent SuspendPropertyChangedEvent(PropertyChangedEventHandler eventHandler)
        {
            return base.SuspendPropertyChangedEvent(eventHandler);
        }
    }
}
