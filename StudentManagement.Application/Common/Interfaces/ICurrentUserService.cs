﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Email { get; }
    }
}
