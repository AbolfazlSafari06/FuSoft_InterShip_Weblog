namespace Weblog
{
    public class Routing
    {
        public class Users
        {
            public const string BaseUser = "users";
            public class Get
            {
                public const string Users = "getlist";
                public const string User = "get/{id}";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Update = "update";
                public const string Dalete = "Delete/{id}";
            }

        }
        public class Category
        {
            public const string BaseUser = "category";
            public class Get
            {
                public const string Categoriy = "{id}";
                public const string GetaPrents = "getparents/{name}";
                public const string GetListOfCategories = "listofnameandid";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Update = "update";
                public const string Dalete = "Delete/{id}";
            }

        }

        public class Auth
        {
            public const string Base = "auth";

            public const string Login = "login";
            public const string Register = "register";
        }

        public class Article
        {
            public const string Base = "article";

            public class Get
            {
                public const string Detail = "{id}";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Edit = "Edit/{id}";
                public const string Delete = "delete/{id}";

            }

        }
    }
}
