using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WhoPK.GameLogic.Core
{
    public readonly struct CommandTable
    {
        readonly CommandType[] command;
    };

    public readonly struct CommandType
    {
        readonly char keyword;
        readonly ICommand command;

    }

}
