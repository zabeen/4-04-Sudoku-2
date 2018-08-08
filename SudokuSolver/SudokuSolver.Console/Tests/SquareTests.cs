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
        public void Square_CreateNew_InitialValueBetween1And9_ValueIsSet(
            [Values(true, false)] bool isChangeable, 
            [Range(1, 9, 1)] int testValue)
        {
            var square = new Square(isChangeable, testValue);
            square.TryChangeValue(testValue);

            square.Value.Should().Be(testValue);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void Square_CreateNew_InitialValueLessThan1OrGreaterThan9_ThrowsException(
            [Values(true, false)] bool isChangeable,
            int testValue)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var square = new Square(isChangeable, testValue);
            });
        }

        [Test]
        public void Square_ValueIsChangeable_TryChangeValue_Between1And9_ValueIsChanged(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);
            square.TryChangeValue(testValue);

            square.Value.Should().Be(testValue);
        }

        [Test]
        public void Square_ValueIsChangeable_TryChangeValue_Between1And9_ReturnsTrue(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);
            var result = square.TryChangeValue(testValue);

            result.Should().BeTrue();           
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        public void Square_ValueIsChangeable_TryChangeValue_LessThan1OrGreaterThan9_ThrowsException(int testValue)
        {
            const bool isChangeable = true;
            var square = new Square(isChangeable);

            Assert.Throws<ArgumentException>(() => square.TryChangeValue(testValue));
        }

        [Test]
        public void Square_ValueIsNotChangeable_TryChangeValue_Between1And9_ValueDoesNotChange(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = false;
            var square = new Square(isChangeable);
            var originalValue = square.Value;

            square.TryChangeValue(testValue);

            square.Value.Should().Be(originalValue);
        }

        [Test]
        public void Square_ValueIsNotChangeable_TryChangeValue_Between1And9_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            const bool isChangeable = false;
            var square = new Square(isChangeable);
            var result = square.TryChangeValue(testValue);

            result.Should().BeFalse();
        }
    }
}
