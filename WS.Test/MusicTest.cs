using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Music.Controllers;
using WS.Music.Stores;
using Xunit;

namespace WS.Test
{
    [Collection("TestMusicCollection")]
    public class MusicTest
    {
        private readonly TestBase<MusicDbContext> _testBase;

        [Fact]
        public void TestSongList()
        {
            var api = _testBase.ServiceProvider.GetRequiredService<ApiController>();
            var result = api.Check();
            Assert.Equal("0", result.Code);
        }

    }
}
