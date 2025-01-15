using NSubstitute;

namespace TagCloudTests;

[TestFixture]
public class BaseTest<T> where T : class
{
	protected T Sut { get; set; }
	private object[] _constructorParameters;

	[SetUp]
	public virtual void SetUp()
	{
		_constructorParameters = typeof(T)
			.GetConstructors()
			.First()
			.GetParameters()
			.Select(method => Substitute.For([method.ParameterType], null))
			.ToArray();
		Sut = Substitute.ForPartsOf<T>(_constructorParameters);
	}

	protected TInjectedService Mock<TInjectedService>()
	{
		var mock = _constructorParameters.FirstOrDefault(x => x is TInjectedService);

		if (mock == null)
			throw new Exception($"Класс {nameof(TInjectedService)} не используется в классе {nameof(T)}");

		return (TInjectedService)mock;
	}
}