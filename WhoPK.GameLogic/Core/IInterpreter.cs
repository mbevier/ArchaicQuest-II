using System;
using System.Collections.Generic;
using System.Text;
using static WhoPK.GameLogic.Core.Interpreter;

namespace WhoPK.GameLogic.Core
{
    public interface IInterpreter
    {
        public InterpretedInput Interpret(string input);

        public string ParseSingleArgument(string input, out string remainingArgument);
    }
}
