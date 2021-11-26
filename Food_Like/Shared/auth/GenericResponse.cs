using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class GenericResponse<T>
    {
        public bool Sucess { get; set; }
        public T Data { get; set; }


    }
}
