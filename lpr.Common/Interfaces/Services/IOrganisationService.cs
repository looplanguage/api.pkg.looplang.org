﻿using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services
{
    public interface IOrganisationService
    {
        void AddOrganisation(string Name, string UserId);
        Organisation GetOrganisation(string OrgId);
    }
}
