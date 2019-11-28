using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using Attribute = ABAC.DAL.Entities.Attribute;

namespace ABAC.DAL.Extensions
{
    public static class AttributeExtension
    {
        public static User SetDefaultAttributes(this User user)
        {
            user.Attributes = DefaultUserAttributes;

            return user;
        }

        public static User SetInfo(this User user, UserInfo info)
        {
            user.Name = info.Name;

            return user;
        }

        public static User SetCredentials(this User user, UserCredentials credentials)
        {
            user.Login = credentials.Login;
            user.Password = credentials.Password;

            return user;
        }

        public static Resource SetDefaultAttributes(this Resource resource)
        {
            resource.Attributes = DefaultResourceAttributes;

            return resource;
        }

        public static Resource SetInfo(this Resource resource, ResourceInfo info)
        {
            resource.Name = info.Name;
            resource.Value = info.Value;

            return resource;
        }

        private static ICollection<Attribute> DefaultUserAttributes
            => new List<Attribute> { new Attribute { Name = "User.Role", Value = "User" } };

        private static ICollection<Attribute> DefaultResourceAttributes
            => new List<Attribute> { new Attribute { Name = "Resource.CreatedAt", Value = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) } };
    }
}
