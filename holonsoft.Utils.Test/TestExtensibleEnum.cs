using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace holonsoft.Utils.Test;

public record Cameras : ExtensibleEnum<Cameras>
{
  public static readonly Cameras Undefined = new(0, nameof(Undefined));

  public Cameras(int value, string name) : base(value, name) { }
}

public record ExtendedCameras : Cameras
{
  public static readonly ExtendedCameras TopCamera = new(1, nameof(TopCamera));
  public static readonly ExtendedCameras BottomCamera = new(2, nameof(BottomCamera));
  public static readonly ExtendedCameras FrontCamera = new(3, nameof(FrontCamera));

  public ExtendedCameras(int value, string name) : base(value, name)
  {
  }
}

public class TestExtensibleEnum
{
  [Fact]
  public void TestEnumCollectionFunctionality()
  {
    Cameras.GetValues().Should().BeEquivalentTo(ExtendedCameras.GetValues());
    Cameras.GetNames().Should().BeEquivalentTo(ExtendedCameras.GetNames());
    Cameras.GetUnderlyingValues().Should().BeEquivalentTo(ExtendedCameras.GetUnderlyingValues());

    Cameras.GetValues().Should().HaveCount(4);

    Cameras.TryParse("InvalidValue", out _).Should().BeFalse();
    Cameras.TryGetValueByUnderlyingValue(-1, out _).Should().BeFalse();

    ExtendedCameras.TryParse("InvalidValue", out _).Should().BeFalse();
    ExtendedCameras.TryGetValueByUnderlyingValue(-1, out _).Should().BeFalse();

    Action shouldThrow = () => Cameras.Parse("InvalidValue");
    shouldThrow.Should().Throw<KeyNotFoundException>();
    shouldThrow = () => Cameras.GetValueByUnderlyingValue(-1);
    shouldThrow.Should().Throw<KeyNotFoundException>();

    shouldThrow = () => ExtendedCameras.Parse("InvalidValue");
    shouldThrow.Should().Throw<KeyNotFoundException>();
    shouldThrow = () => ExtendedCameras.GetValueByUnderlyingValue(-1);
    shouldThrow.Should().Throw<KeyNotFoundException>();
  }

  [Theory]
  [InlineData(0, nameof(Cameras.Undefined), typeof(Cameras))]
  [InlineData(1, nameof(ExtendedCameras.TopCamera), typeof(ExtendedCameras))]
  [InlineData(2, nameof(ExtendedCameras.BottomCamera), typeof(ExtendedCameras))]
  [InlineData(3, nameof(ExtendedCameras.FrontCamera), typeof(ExtendedCameras))]
  public void TestEnumValueFunctionality(int internalValue, string name, Type type)
  {
    Cameras.GetValues().Where(x => x.Value == internalValue && x.Name == name).Should().HaveCount(1);
    Cameras.TryParse(name, out var cameraByParsing).Should().BeTrue();
    Cameras.TryGetValueByUnderlyingValue(internalValue, out var cameraByValue).Should().BeTrue();
    cameraByParsing.Should().BeEquivalentTo(cameraByValue);
    cameraByValue.Value.Should().Be(internalValue);
    cameraByValue.Name.Should().Be(name);
    cameraByValue.GetType().Should().Be(type);

    var asString = JsonSerializer.Serialize(cameraByValue);
    var deserialized = JsonSerializer.Deserialize<Cameras>(asString);
    deserialized.Value.Should().Be(cameraByValue.Value);
    deserialized.Name.Should().Be(cameraByValue.Name);

    var newedUp = new Cameras(internalValue, name);
    newedUp.Should().BeEquivalentTo(deserialized);
    (newedUp == deserialized).Should().BeTrue();
    newedUp.Equals(deserialized).Should().BeTrue();
  }

  [Fact]
  public void TestEnumComparer()
  {
    ExtendedCameras.TopCamera.CompareTo(Cameras.Undefined).Should().BeGreaterThan(0);
    Cameras.Undefined.CompareTo(ExtendedCameras.TopCamera).Should().BeLessThan(0);

    ExtendedCameras.BottomCamera.CompareTo(ExtendedCameras.BottomCamera).Should().Be(0);

    var cameras = new[]
    {
      ExtendedCameras.FrontCamera, ExtendedCameras.BottomCamera, ExtendedCameras.TopCamera, Cameras.Undefined,
      ExtendedCameras.FrontCamera
    };

    // equality
    var distinctCameras = cameras.Distinct();
    distinctCameras.Count().Should().Be(4);

    // hierarchy
    var sortedCameras = cameras.Select((camera, index) => (index, camera)).OrderBy(x => x.camera).Select(x => x.camera)
      .ToArray();
    var expectedOrder = new[]
    {
      Cameras.Undefined, ExtendedCameras.TopCamera, ExtendedCameras.BottomCamera, ExtendedCameras.FrontCamera,
      ExtendedCameras.FrontCamera
    };
    sortedCameras.Should().BeEquivalentTo(expectedOrder);

    // hash
    var cameraSet = cameras.ToHashSet();
    cameraSet.Count.Should().Be(4);
  }
}
