using FluentAssertions;
using NUnit.Framework;
using SearchFight.Exceptions;
using SearchFight.Validators;
using System;

namespace SearchFight.UnitTests.ValidatorTests
{
    [TestFixture]
    public class SearchFightValidatorTests
    {
        private SearchFightValidator _validator;

        [SetUp]
        public void Config()
        {
            _validator = new SearchFightValidator();
        }


        [Test]
        public void Validate_NullParam_ThrowsValidatorException()
        {
            //Arrange            

            //Action
            Action result = () => _validator.Validate(null);

            //Assert
            result.Should().Throw<ValidatorException>().WithMessage("Technologies should have at least 2 values to compare");
        }

        [Test]
        public void Validate_EmptyArray_ThrowsValidatorException()
        {
            //Arrange            
            var param = new string[0];

            //Action
            Action result = () => _validator.Validate(param);

            //Assert
            result.Should().Throw<ValidatorException>().WithMessage("Technologies should have at least 2 values to compare");
        }
        [Test]
        public void Validate_ArrayWith1Element_ThrowsValidatorException()
        {
            //Arrange            
            var param = new string[1] { "c#" };

            //Action
            Action result = () => _validator.Validate(param);

            //Assert
            result.Should().Throw<ValidatorException>().WithMessage("Technologies should have at least 2 values to compare");
        }

        [Test]
        public void Validate_DuplicateElementsInArray_ThrowsValidatorException()
        {
            //Arrange            
            var param = new string[2] { "c#", "c#" };

            //Action
            Action result = () => _validator.Validate(param);

            //Assert
            result.Should().Throw<ValidatorException>().WithMessage("Technologies should not have duplicate values to compare");
        }

        [Test]
        public void Validate_NoDuplicateElementsInArray_ThrowsValidatorException()
        {
            //Arrange            
            var param = new string[2] { "c#", "js" };

            //Action
            Action result = () => _validator.Validate(param);

            //Assert
            result.Should().NotThrow();
        }
    }
}
