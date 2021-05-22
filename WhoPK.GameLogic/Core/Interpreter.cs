using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WhoPK.GameLogic.Core
{
    public class Interpreter : IInterpreter
    {
        public struct InterpretedInput
        {
            public string commandName;
            public string argument;
        }

        public InterpretedInput Interpret (string input)
        {
            input.Trim();
            int index = 0;
            int segmentIndex = 0;
            string command = String.Empty;
            string argument = String.Empty;
          
            command = ParseSingleArgument(input, out argument);

            return new InterpretedInput()
            {
                commandName = command,
                argument = argument
            };
        }

        public string ParseSingleArgument (string input, out string remainingArgument)
        {
            if (input.Length == 0)
            {
                remainingArgument = String.Empty;
                return String.Empty;
            }

            int index = 0;
            input = input.Trim();

            bool isSingleQuotedParameter = (input[index] == '\'');
            bool isDoubleQuotedParamater = (input[index] == '"');
            if (isSingleQuotedParameter || isDoubleQuotedParamater)
            {
                index++;
                while (index < input.Length && !(input[index] == '\'' || input[index] == '"'))
                {
                    index++;
                }
                int buffer = isDoubleQuotedParamater ? 1 : 1;
                remainingArgument = input.Substring(index + buffer);
                return input.Substring(1, index - 1).Trim();
            }
            else
            {
                while (index<input.Length && (!char.IsWhiteSpace(input[index])))
                {
                    index++;
                }
                remainingArgument = input.Substring(index).Trim();
                return input.Substring(0, index);
            }
        }
    }
}
