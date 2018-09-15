using System;
using RAExt;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var test = new ItemData().ToUnmanaged();
        }
    }
}