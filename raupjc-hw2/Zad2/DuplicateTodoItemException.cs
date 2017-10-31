using System;

namespace Zad2
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(string message)
            :base(message)
        {
        }
    }
}