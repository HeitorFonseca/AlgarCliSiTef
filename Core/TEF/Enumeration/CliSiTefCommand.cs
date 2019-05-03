using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TEF.Enumeration
{
    public enum CliSiTefCommand
    {
        ValueToBeStored = 0,
        MessageToOperator = 1,
        MessageToClient = 2,
        MessageToOperatorAndClient = 3,

        UserSelectYesNoPinPad = 20,
        UserSelectOptionsMenuPinPad = 21,

    }
}
