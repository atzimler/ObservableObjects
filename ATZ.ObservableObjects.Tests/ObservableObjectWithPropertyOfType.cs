namespace ATZ.ObservableObjects.Tests
{
    public class ObservableObjectWithPropertyOfType<T> : ObservableObject
    {
        private T? _property;

        public T? Property
        {
            get => _property;
            set => Set(ref _property, value);
        }
    }
}
