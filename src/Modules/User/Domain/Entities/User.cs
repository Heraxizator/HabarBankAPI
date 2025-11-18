using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Enums;

namespace Users.Domain.Entities;

public class User : BaseEntity
{
    public new long Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public long RoleId { get; set; }
}