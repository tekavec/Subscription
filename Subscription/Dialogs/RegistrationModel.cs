﻿using System;

namespace Subscription.Dialogs
{
    public class RegistrationModel
    {
        public string Pin { get; }
        public string PinRepeat { get; }

        public RegistrationModel(string pin, string pinRepeat)
        {
            Pin = pin;
            PinRepeat = pinRepeat;
        }
    }
}