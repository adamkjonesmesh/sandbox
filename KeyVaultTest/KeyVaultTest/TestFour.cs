using Xunit;

namespace KeyVaultTest
{
    public class TestFour : IClassFixture<GenericStartupFixture>
    {
        GenericStartupFixture _fixture;

        public TestFour(GenericStartupFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShowValueFour()
        {
            Assert.Equal("MeshDev", _fixture.EnvironmentContext);
        }
    }
}
