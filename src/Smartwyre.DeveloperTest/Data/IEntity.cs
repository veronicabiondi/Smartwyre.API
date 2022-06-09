using System;

namespace Smartwyre.DeveloperTest.Data
{
    public interface IEntity
    {
        int Id { get; set; }

        byte[] Version { get; set; }

        int? InsertUserId { get; set; }

        DateTime? InsertDateTime { get; set; }

        int? LastModifiedUserId { get; set; }

        DateTime? LastModifiedDateTime { get; set; }
    }
}