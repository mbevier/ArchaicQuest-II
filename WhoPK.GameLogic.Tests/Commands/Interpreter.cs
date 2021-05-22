using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using WhoPK.GameLogic.Core;
using Xunit;

namespace WhoPK.GameLogic.Tests.Commands
{
    public class InterpreterTests
    {
        public IInterpreter interpreter;

        public InterpreterTests()
        {
            interpreter = new Interpreter();
        }

        [Fact]
        public void Interpret_With_No_Arguments_IsSuccessful()
        {
            var output = interpreter.Interpret("look");
            Assert.Equal("look", output.commandName);
        }

        [Fact]
        public void Interpret_With_One_Argument_IsSuccessful()
        {
            var output = interpreter.Interpret("look self");
            Assert.Equal("look", output.commandName);
            Assert.Equal("self", output.argument);
        }

        [Fact]
        public void Interpret_With_Single_Quote_Argument_IsSuccessful()
        {
            var output = interpreter.Interpret("cast 'cure blindness'");
            Assert.Equal("cast", output.commandName);
            Assert.Equal("'cure blindness'", output.argument);
        }

        [Fact]
        public void ParseSingleArgument_With_Single_Quote_Argument_IsSuccessful()
        {
            var output = interpreter.Interpret("cast 'cure blindness'");
            var remainingArgument = "Remainder";
            var singleArgument = interpreter.ParseSingleArgument(output.argument, out remainingArgument);
            Assert.Equal("cast", output.commandName);
            Assert.Equal("cure blindness", singleArgument);
            Assert.Equal(String.Empty, remainingArgument);
        }

        [Fact]
        public void ParseSingleArgument_With_Double_Quote_Argument_IsSuccessful()
        {
            var output = interpreter.Interpret("cast \"cure blindness\"");
            var remainingArgument = "Remainder";
            var singleArgument = interpreter.ParseSingleArgument(output.argument, out remainingArgument);
            Assert.Equal("cast", output.commandName);
            Assert.Equal("cure blindness", singleArgument);
            Assert.Equal(String.Empty, remainingArgument);
        }

        [Theory]
        [InlineData(@"cast ""cure blindness"" target", "cast", "cure blindness", "target")]
        public void ParseSingleArgument_With_MultipleArguments_IsSuccessful(string input, string command, string firstArgument, string secondArgument)
        {
            var output = interpreter.Interpret(input);
            var remainingArgument = "Remainder";
            var firstSingleArgument = interpreter.ParseSingleArgument(output.argument, out remainingArgument);
            var secondSingleArgument = interpreter.ParseSingleArgument(remainingArgument, out remainingArgument);
            Assert.Equal(command, output.commandName);
            Assert.Equal(firstArgument, firstSingleArgument);
            Assert.Equal(secondArgument, secondSingleArgument);
            Assert.Equal(String.Empty, remainingArgument);
        }

    }
}

