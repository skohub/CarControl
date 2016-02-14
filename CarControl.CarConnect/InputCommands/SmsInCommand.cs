﻿using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class SmsInCommand : IInputCommand
    {
        private readonly ISmsService _smsService;
        private readonly int _carId;
        private readonly string _text;
        private readonly DateTime _time;

        public SmsInCommand(ISmsService smsService, int carId, string text, DateTime time)
        {
            _smsService = smsService;
            _carId = carId;
            _text = text;
            _time = time;
        }

        public void Execute()
        {
            var sms = new Sms {Direction = "IN", CarId = _carId, Text = _text, Time = _time};
            _smsService.CreateSms(sms);
        }
    }
}