using System;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Data
{ 
    public abstract class Entity : IEntity
    {
        public abstract int Id { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        public int? InsertUserId { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public int? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}