using System.ComponentModel;
using JetBrains.Annotations;
using Xunit;

namespace ATZ.ObservableObjects.Tests;

public class ObservableObjectShould
{
    private int _callCounter;

    private void CallCounter(object? sender, PropertyChangedEventArgs e)
    {
        _callCounter++;
        Assert.NotNull(e);
        Assert.Equal("PropertyRaisingChangeNotification", e.PropertyName);
    }

    [Fact]
    public void FireAdditionalPropertyChangedProperly()
    {
        var eventAFired = false;
        var eventBFired = false;

        var vm = new TestObservableObject();
        vm.PropertyChanged += (_, e) =>
        {
            Assert.NotNull(e);
            if (e.PropertyName == "A") eventAFired = true;
        };
        vm.PropertyChanged += (_, e) =>
        {
            Assert.NotNull(e);
            if (e.PropertyName == "B") eventBFired = true;
        };

        vm.A++;

        Assert.True(eventAFired);
        Assert.True(eventBFired);
    }

    [Fact]
    public void FirePropertyChangeNotificationWhenUsingSetWithTwoParameters()
    {
        var eventFired = false;

        var vm = new TestObservableObject();
        vm.PropertyChanged += (_, [UsedImplicitly]e) =>
        {
            Assert.NotNull(e);
            Assert.Equal("SetWith2Parameters", e.PropertyName);
            eventFired = true;
        };

        Assert.NotEqual(13, vm.PropertyRaisingChangeNotification);

        vm.SetWith2Parameters(13);

        Assert.True(eventFired);
    }

    [Fact]
    public void NotCrashFromANullableProperty()
    {
        const int value = 13;
        var oo = new ObservableObjectWithPropertyOfType<int?>();

        Act();

        Assert.Equal(value, oo.Property);
        return;
        
        void Act() => oo.Property = value;
    }

    [Fact]
    public void NotCrashFromAStringProperty()
    {
        const string value = "Property";
        var oo = new ObservableObjectWithPropertyOfType<string>();
        
        Act();

        Assert.Equal(value, oo.Property);
        return;
        
        void Act() => oo.Property = value;
    }

    [Fact]
    public void NotCrashIfAdditionalPropertiesChangedIsNull()
    {
        var vm = new TestObservableObject();
        vm.NullAdditionalProperties();
    }

    [Fact]
    public void NotFirePropertyChangedWhenValueSetIsTheSame()
    {
        var vm = new TestObservableObject();
        vm.PropertyChanged += CallCounter;

        Assert.Equal(0, _callCounter);

        var value = vm.PropertyRaisingChangeNotification;
        vm.PropertyRaisingChangeNotification = value;

        Assert.Equal(0, _callCounter);
    }

    [Fact]
    public void ProperlySuspendPropertyChangedEvent()
    {
        var vm = new TestObservableObject();
        vm.PropertyChanged += CallCounter;

        Assert.Equal(0, _callCounter);

        using (vm.SuspendPropertyChangedEvent(CallCounter))
        {
            vm.PropertyRaisingChangeNotification++;
            Assert.Equal(0, _callCounter);
        }

        vm.PropertyRaisingChangeNotification++;
        Assert.Equal(1, _callCounter);
    }

    [Fact]
    public void AllowHookingIntoThePropertyChangedProcessing()
    {
        var vm = new TestObservableObject();

        Assert.Equal(0, vm.OnPropertyChangedCallCount);

        vm.A = 1;
        Assert.Equal(2, vm.OnPropertyChangedCallCount);
    }
}