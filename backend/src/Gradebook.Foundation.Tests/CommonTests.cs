using Gradebook.Foundation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0618

namespace Gradebook.Foundation.Tests
{
    [Category("Unit")]
    public class CommonTests
    {
        [Test]
        public void ShouldSetFakeTime()
        {
            var fakeTime = DateTime.UtcNow.AddHours(5);

            Time.SetFakeUtcNow(fakeTime);
            Assert.That(Time.UtcNow, Is.EqualTo(fakeTime));
            Assert.That(Time.UtcNow, Is.GreaterThan(DateTime.UtcNow));
        }
        [Test]
        public void ShouldSetCorrectTime()
        {
            Time.Reset();
            Assert.That(Time.UtcNow.Subtract(DateTime.UtcNow), Is.LessThan(TimeSpan.FromMilliseconds(10)));
        }
        [Test]
        public void ShouldSetFakeTimeThenShouldSetCorrectTime()
        {
            var fakeTime = DateTime.UtcNow.AddHours(5);
            Time.SetFakeUtcNow(fakeTime);
            Assert.That(Time.UtcNow, Is.EqualTo(fakeTime));
            Assert.That(Time.UtcNow, Is.GreaterThan(DateTime.UtcNow));
            Time.Reset();
            Assert.That(Time.UtcNow.Subtract(DateTime.UtcNow), Is.LessThan(TimeSpan.FromMilliseconds(10)));
        }
    }
}
