using Xunit;

namespace KeyVaultTest
{
    public class TestTwo : IClassFixture<GenericStartupFixture>
    {
        GenericStartupFixture _fixture;

        public TestTwo(GenericStartupFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShowValueTwo()
        {
            Assert.Equal("MeshDev", _fixture.EnvironmentContext);
        }
    }
}
