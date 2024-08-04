using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.ModelsDTO
{
    public class OperationSuccessDTO<T> : OperationResultDTO where T : class
    {
        public T Result { get; set; }
    }
}
