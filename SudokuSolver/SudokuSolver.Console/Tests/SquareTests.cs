using FluentAssertions;
using NUnit.Framework;
using SudokuSolver.Console.Models;
using System;

namespace SudokuSolver.Console.Tests
{
    [TestFixture]
    public class SquareTests
    {
        [Test]
        public void Square_CreateNew_NoValue_ValueIsChangeableIsTrue()
        {
            var square = new Square();

            square.ValueIsChangeable.Should().BeTrue();
        }

        [Test]
        public void Square_CreateNew_ZeroValue_ValueIsChangeableIsTrue()
        {
            var square = new Square(0);

            square.ValueIsChangeable.Should().BeTrue();
        }

        [Test]
        public void Square_CreateNew_NonZeroValue_ValueIsChangeableIsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(testValue);

            square.ValueIsChangeable.Should().BeFalse();
        }

        [Test]
        public void Square_CreateNew_ValueBetween1And9_ValueIsSet(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(testValue);

            square.Value.Should().Be(testValue);
        }

        [Test]
        public void Square_CreateNew_ValueLessThan0OrGreaterThan9_ThrowsException(
            [Values(-1, 10)] int testValue)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var square = new Square(testValue);
            });
        }

        [Test]
        public void Square_TryChangeValue_SquareIsChangeable_AndValueBetween1And9_ValueIsChanged(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square();
            square.TryChangeValue(testValue);

            square.Value.Should().Be(testValue);
        }

        [Test]
        public void Square_TryChangeValue_SquareIsChangeable_AndValueBetween1And9_ReturnsTrue(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square();
            var result = square.TryChangeValue(testValue);

            result.Should().BeTrue();
        }

        public void Square_TryChangeValue_SquareIsChangeable_AndValueLessThan1OrGreaterThan9_ThrowsException(
            [Values(-1, 0, 10)] int testValue)
        {
            var square = new Square();

            Assert.Throws<ArgumentException>(() => square.TryChangeValue(testValue));
        }

        [Test]
        public void Square_TryChangeValue_SquareIsFixed_AndValueBetween1And9_ValueDoesNotChange(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(1);
            var originalValue = square.Value;

            square.TryChangeValue(testValue);

            square.Value.Should().Be(originalValue);
        }

        [Test]
        public void Square_TryChangeValue_SquareIsFixed_AndValueBetween1And9_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(1);

            var result = square.TryChangeValue(testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void Square_CreateNew_NoValue_IsEmptyIsTrue()
        {
            var square = new Square();

            square.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Square_CreateNew_ZeroValue_IsEmptyIsTrue()
        {
            var square = new Square(0);

            square.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Square_CreateNew_NonZeroValue_IsEmptyIsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(testValue);

            square.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Square_TryChangeValue_SquareIsChangeable_AndValueBetween1And9_IsEmptyIsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square();

            square.TryChangeValue(testValue);

            square.IsEmpty().Should().BeFalse();
        }
    }
}
