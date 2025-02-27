using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarExample
{
    public interface IInputBoardObserver
    {
        void OnMenValueChanged(Position position, String value);
    }
}
