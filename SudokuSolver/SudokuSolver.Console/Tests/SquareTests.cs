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
        public void Square_CreateNew_ValueIsChangeable_ValueIsChangeableIsTrue()
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);

            square.ValueIsChangeable.Should().BeTrue();
        }

        [Test]
        public void Square_CreateNew_ValueIsNotChangeable_ValueIsChangeableIsFalse()
        {
            const bool isChangeable = false;
            var square = new Square(isChangeable);

            square.ValueIsChangeable.Should().BeFalse();
        }

        [Test]
        public void Square_CreateNew_InitialValueBetween0And9_ValueIsSet(
            [Values(true, false)] bool isChangeable, 
            [Range(0, 9, 1)] int testValue)
        {
            var square = new Square(isChangeable, testValue);
            square.TryChangeValue(testValue);

            square.Value.Should().Be(testValue);
        }

        [Test]
        public void Square_CreateNew_InitialValueLessThan0OrGreaterThan9_ThrowsException(
            [Values(true, false)] bool isChangeable,
            [Values(-1, 10)] int testValue)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var square = new Square(isChangeable, testValue);
            });
        }

        [Test]
        public void Square_ValueIsChangeable_TryChangeValue_Between0And9_ValueIsChanged(
            [Range(0, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);
            square.TryChangeValue(testValue);

            square.Value.Should().Be(testValue);
        }

        [Test]
        public void Square_ValueIsChangeable_TryChangeValue_Between0And9_ReturnsTrue(
            [Range(0, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);
            var result = square.TryChangeValue(testValue);

            result.Should().BeTrue();           
        }

        public void Square_ValueIsChangeable_TryChangeValue_LessThan0OrGreaterThan9_ThrowsException(
            [Values(-1, 10)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);

            Assert.Throws<ArgumentException>(() => square.TryChangeValue(testValue));
        }

        [Test]
        public void Square_ValueIsNotChangeable_TryChangeValue_Between0And9_ValueDoesNotChange(
            [Range(0, 9, 1)] int testValue)
        {
            const bool isChangeable = false;
            var square = new Square(isChangeable);
            var originalValue = square.Value;

            square.TryChangeValue(testValue);

            square.Value.Should().Be(originalValue);
        }

        [Test]
        public void Square_ValueIsNotChangeable_TryChangeValue_Between0And9_ReturnsFalse(
            [Range(0, 9, 1)] int testValue)
        {
            const bool isChangeable = false;
            var square = new Square(isChangeable);
            var result = square.TryChangeValue(testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void Square_IsEmpty_WhenValueNotSet_ReturnsTrue()
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);

            square.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Square_IsEmpty_WhenValueChangedToZero_ReturnsTrue()
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable, 1);
            square.TryChangeValue(0);

            square.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Square_IsEmpty_WhenValueSetToPermittedNonZeroNumber_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable, testValue);

            square.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Square_IsEmpty_WhenValueChangedToPermittedNonZeroNumber_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);
            square.TryChangeValue(testValue);

            square.IsEmpty().Should().BeFalse();
        }
    }
}
