using LanchoneteConsole.Mapeamento;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Interface
{
    internal interface IDAO<T>
    {
        void Insert(T t);
        void Update(T t);
        void Delete(int t);
        List<T> GetAll();
    }
}
