using DNA_Project.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNA_Project.DNA
{
    internal abstract class Node
    {
        abstract public Task<Object> Process(String _content);
    }
}