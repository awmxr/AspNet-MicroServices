using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exeptions
{
    public class NotFoundExeption : ApplicationException
    {
        public NotFoundExeption(string name , object key ):base($"Entity \"{name}\" and key \"{key}\" was not found")
        {
            
        }
    }
}
