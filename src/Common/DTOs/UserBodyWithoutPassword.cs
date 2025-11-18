using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs;

public class UserBodyWithotPassword
{
    public long Id { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public long RoleId { get; set; }
}