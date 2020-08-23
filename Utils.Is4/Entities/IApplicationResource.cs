using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Is4.Entities
{
    interface IApplicationResource
    {
        string ApplicationName { get; set; }
        Application Application { get; set; }
    }
}
