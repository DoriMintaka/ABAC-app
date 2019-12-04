using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using System;
using System.Globalization;

namespace ABAC.DAL.Extensions
{
    public static class AttributeExtension
    {
        public static User SetDefaultAttributes(this User user)
        {
            user["role"] = "user";

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
            resource["createdat"] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            return resource;
        }

        public static Resource SetInfo(this Resource resource, ResourceInfo info)
        {
            resource.Name = info.Name;
            resource.Value = info.Value;

            return resource;
        }
    }
}
