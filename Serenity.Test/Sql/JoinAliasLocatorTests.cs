﻿using System;
using Xunit;

namespace Serenity.Data.Test
{
    public partial class JoinAliasLocatorTests
    {
        [Fact]
        public void LocateOptimizedWithNullExpressionThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => {
                string singleAlias;
                JoinAliasLocator.LocateOptimized(null, out singleAlias);
            });
        }

        [Fact]
        public void LocateOptimizedWorksWithEmptyString()
        {
            string singleAlias;
            var aliases = JoinAliasLocator.LocateOptimized("", out singleAlias);
            Assert.Equal(null, aliases);
            Assert.Equal(null, singleAlias);
        }

        [Fact]
        public void LocateOptimizedReturnsSingleAliasIfExpressionContainsSingle()
        {
            string singleAlias;
            var aliases = JoinAliasLocator.LocateOptimized("x.y", out singleAlias);
            Assert.Equal(null, aliases);
            Assert.Equal("x", singleAlias);
        }

        [Fact]
        public void LocateOptimizedReturnsSingleAliasIfExpressionContainsSingleAliasDoubleTimes()
        {
            string singleAlias;
            var aliases = JoinAliasLocator.LocateOptimized("x.y + x.z", out singleAlias);
            Assert.Equal(null, aliases);
            Assert.Equal("x", singleAlias);
        }

        [Fact]
        public void LocateOptimizedReturnsHashSetIfExpressionContainsMultipleAliases()
        {
            string singleAlias;
            var aliases = JoinAliasLocator.LocateOptimized("x.y + y.z", out singleAlias);
            Assert.Equal(null, singleAlias);
            Assert.Equal(2, aliases.Count);
            Assert.True(aliases.Contains("x"));
            Assert.True(aliases.Contains("y"));
        }

        [Fact]
        public void LocateOptimizedReturnsHashSetWithIgnoreCase()
        {
            string singleAlias;
            var aliases = JoinAliasLocator.LocateOptimized("x.y + y.z", out singleAlias);
            Assert.Equal(null, singleAlias);
            Assert.Equal(2, aliases.Count);
            Assert.True(aliases.Contains("X"));
            Assert.True(aliases.Contains("x"));
            Assert.True(aliases.Contains("Y"));
            Assert.True(aliases.Contains("y"));
        }
    }
}