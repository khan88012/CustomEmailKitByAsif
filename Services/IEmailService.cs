﻿using CustomEmailKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEmailKit.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
