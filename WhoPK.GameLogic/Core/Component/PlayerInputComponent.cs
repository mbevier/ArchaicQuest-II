using Artemis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WhoPK.GameLogic.Commands.Movement;

namespace WhoPK.GameLogic.Core.Component
{
    public class PlayerInputComponent : ComponentPoolable
    {
        public Stack<string> commands = new Stack<string>();
        public Guid userId;
        public string connectionId;
        public bool locked = false;
    }
}
