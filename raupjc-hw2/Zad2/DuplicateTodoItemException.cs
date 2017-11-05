using System;

namespace Zad2
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(Guid id)
            :base($"duplicate id: {id}")
        {
        }
    }
}