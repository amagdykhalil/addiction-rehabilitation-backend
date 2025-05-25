using ARC.Domain.Interfaces;

namespace ARC.Domain.Abstract
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}



