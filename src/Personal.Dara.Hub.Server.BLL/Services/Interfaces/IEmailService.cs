﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string email);
    }
}
