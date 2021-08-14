using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
    public interface IRazorEngine
    {
        Task<string> ParseAsync<TModel>(string viewName, TModel model);
    }
}
