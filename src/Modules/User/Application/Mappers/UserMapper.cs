using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;

namespace Users.Application.Mappers;

public static class MUser
{
    public static User GetModelWithoutPassword(UserBodyWithotPassword body)
    {
        return new User()
        {
            Id = body.Id,
            Login = body.Login,
            Email = body.Email,
            FirstName = body.FirstName,
            MiddleName = body.MiddleName,
            LastName = body.LastName,
            RoleId = body.RoleId
        };
    }

    public static User GetModelWithPassword(UserBodyWithPassword body)
    {
        return new User()
        {
            Id = body.Id,
            Login = body.Login,
            Password = body.Password,
            Email = body.Email,
            FirstName = body.FirstName,
            MiddleName = body.MiddleName,
            LastName = body.LastName,
            RoleId = body.RoleId
        };
    }

    public static UserBodyWithotPassword GetBodyWithoutPassword(User model)
    {
        return new UserBodyWithotPassword()
        {
            Id = model.Id,
            LastName = model.LastName,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            Email = model.Email,
            Login = model.Login,
            RoleId = model.RoleId
        };
    }

    public static UserBodyWithPassword GetBodyWithPassword(User model)
    {
        return new UserBodyWithPassword()
        {
            Id = model.Id,
            LastName = model.LastName,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            Email = model.Email,
            Login = model.Login,
            Password = model.Password,
            RoleId = model.RoleId,
        };
    }
}
